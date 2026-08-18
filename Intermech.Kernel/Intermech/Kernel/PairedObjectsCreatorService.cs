// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.PairedObjectsCreatorService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel;

internal sealed class PairedObjectsCreatorService : LongLifeObject, IPairedObjectsCreatorService
{
  private static readonly Type BaseCreatorType = typeof (PairedObjectsCreator);
  private static readonly object dataKey = new object();
  private readonly List<Func<PairedObjectsCreator>> factories;
  private readonly object syncRoot;

  public PairedObjectsCreatorService(IEventLogHelper eventlogHelper)
  {
    if (eventlogHelper == null)
      throw new ArgumentNullException(nameof (eventlogHelper));
    this.factories = new List<Func<PairedObjectsCreator>>();
    this.syncRoot = new object();
    eventlogHelper.CreateObjectEvent += new ObjectEventHandler(this.OnAfterCreateBlank);
    eventlogHelper.AfterCreateObjectEvent += new AfterCreateObjectHandler(this.OnAfterCreateObject);
    eventlogHelper.EndCreateObjectEvent += new AfterCreateObjectHandler(this.OnEndCreateObject);
    eventlogHelper.CancelCreateObjectEvent += new BoundaryCreateObjectHandler(this.OnCancelCreateObject);
  }

  public void RegisterCreator(Type creatorType)
  {
    if (creatorType == (Type) null)
      throw new ArgumentNullException(nameof (creatorType));
    if (!creatorType.IsSubclassOf(PairedObjectsCreatorService.BaseCreatorType))
      throw new ArgumentException($"Тип обработчика должен быть унаследован от '{PairedObjectsCreatorService.BaseCreatorType}'.", nameof (creatorType));
    this.RegisterCreator((Func<PairedObjectsCreator>) (() => (PairedObjectsCreator) Activator.CreateInstance(creatorType)));
  }

  public void RegisterCreator(Func<PairedObjectsCreator> factoryMethod)
  {
    if (factoryMethod == null)
      throw new ArgumentNullException(nameof (factoryMethod));
    lock (this.syncRoot)
      this.factories.Add(factoryMethod);
  }

  public bool IsVersionAlreadyCreated(IUserSession session, long parentVersionId)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    long testId = parentVersionId != 0L && parentVersionId != -1L ? Math.Abs(parentVersionId) : throw new ArgumentException("Не задан идентификатор исходной версии объекта.", nameof (parentVersionId));
    return this.GetSessionDataFromEventHandlerContext(session).CreatedObjects.Exists((Predicate<IDBObject>) (item => item.ParentVersionID == testId));
  }

  public IDBObject FindCreatedVersion(IUserSession session, long parentVersionId)
  {
    if (session == null)
      throw new ArgumentNullException(nameof (session));
    long testId = parentVersionId != 0L && parentVersionId != -1L ? Math.Abs(parentVersionId) : throw new ArgumentException("Не задан идентификатор исходной версии объекта.", nameof (parentVersionId));
    return this.GetSessionDataFromEventHandlerContext(session).CreatedObjects.Find((Predicate<IDBObject>) (item => item.ParentVersionID == testId));
  }

  public List<IDBObject> GetCreatedObjects(IUserSession session)
  {
    return session != null ? new List<IDBObject>((IEnumerable<IDBObject>) this.GetSessionDataFromEventHandlerContext(session).CreatedObjects) : throw new ArgumentNullException(nameof (session));
  }

  private PairedObjectsCreatorService.SessionData GetSessionDataFromEventHandlerContext(
    IUserSession session)
  {
    return this.TryGetSessionData(session) ?? throw new InvalidOperationException("Вызов этого метода допускается только из обработчиков событий создания объектов и версий объектов.");
  }

  private void OnAfterCreateBlank(IDBObject newObject, IUserSession session)
  {
    if (this.TryGetSessionData(session) != null)
      return;
    PairedObjectsCreatorService.SessionData data = this.InitializeCreators(newObject, session);
    this.SetSessionData(session, data);
  }

  private PairedObjectsCreatorService.SessionData InitializeCreators(
    IDBObject newObject,
    IUserSession session)
  {
    lock (this.syncRoot)
    {
      PairedObjectsCreatorService.SessionData sessionData = new PairedObjectsCreatorService.SessionData(newObject.ObjectID, this.factories.Count);
      try
      {
        foreach (Func<PairedObjectsCreator> factory in this.factories)
        {
          PairedObjectsCreator pairedObjectsCreator = factory();
          pairedObjectsCreator.Initialize(sessionData.FirstCreatedObjectId, session);
          pairedObjectsCreator.BeginCreation();
          sessionData.ActiveCreators.Add(pairedObjectsCreator);
        }
        return sessionData;
      }
      catch
      {
        foreach (PairedObjectsCreator activeCreator in sessionData.ActiveCreators)
          activeCreator.CancelCreation();
        throw;
      }
    }
  }

  private void OnAfterCreateObject(IDBObject newObject, IDBObject prototype, IUserSession session)
  {
    PairedObjectsCreatorService.SessionData sessionData = this.TryGetSessionData(session);
    if (sessionData == null)
      return;
    sessionData.CreatedObjects.Add(newObject);
    foreach (PairedObjectsCreator activeCreator in sessionData.ActiveCreators)
      activeCreator.RegisterCreatedObject(newObject);
    foreach (PairedObjectsCreator activeCreator in sessionData.ActiveCreators)
      activeCreator.AfterCreateObject(newObject, prototype);
  }

  private void OnEndCreateObject(IDBObject newObject, IDBObject prototype, IUserSession session)
  {
    PairedObjectsCreatorService.SessionData sessionData = this.TryGetSessionData(session);
    if (sessionData == null || sessionData.FirstCreatedObjectId != newObject.ObjectID)
      return;
    foreach (PairedObjectsCreator activeCreator in sessionData.ActiveCreators)
      activeCreator.EndCreation();
    this.RemoveSessionData(session);
  }

  private void OnCancelCreateObject(
    Guid objectVersionGuid,
    int objectType,
    IDBObject prototype,
    IUserSession session)
  {
    PairedObjectsCreatorService.SessionData sessionData = this.TryGetSessionData(session);
    if (sessionData == null)
      return;
    foreach (PairedObjectsCreator activeCreator in sessionData.ActiveCreators)
      activeCreator.CancelCreation();
    this.RemoveSessionData(session);
  }

  private PairedObjectsCreatorService.SessionData TryGetSessionData(IUserSession session)
  {
    return (PairedObjectsCreatorService.SessionData) session.GetSessionPluginsData(PairedObjectsCreatorService.dataKey);
  }

  private void SetSessionData(IUserSession session, PairedObjectsCreatorService.SessionData data)
  {
    session.SetSessionPluginsData(PairedObjectsCreatorService.dataKey, (object) data);
  }

  private void RemoveSessionData(IUserSession session)
  {
    session.SetSessionPluginsData(PairedObjectsCreatorService.dataKey, (object) null);
  }

  private sealed class SessionData : IUserSessionLocalData
  {
    public readonly long FirstCreatedObjectId;
    public readonly List<PairedObjectsCreator> ActiveCreators;
    public readonly List<IDBObject> CreatedObjects;

    public SessionData(long firstCreatedObjectId, int capacity)
    {
      this.FirstCreatedObjectId = firstCreatedObjectId;
      this.ActiveCreators = new List<PairedObjectsCreator>(capacity);
      this.CreatedObjects = new List<IDBObject>();
    }
  }
}
