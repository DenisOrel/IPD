// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.PairedObjectsCreator
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel;

public class PairedObjectsCreator
{
  private List<IDBObject> createdObjects;
  private bool intialized;
  private long firstCreatedObjectId;
  private IUserSession session;

  public PairedObjectsCreator() => this.createdObjects = new List<IDBObject>();

  internal void Initialize(long firstCreatedObjectId, IUserSession session)
  {
    if (this.intialized)
      throw new InvalidOperationException("Обработчик уже был инициализирован.");
    this.firstCreatedObjectId = firstCreatedObjectId;
    this.session = session;
    this.intialized = true;
  }

  internal void RequireInitialized()
  {
    if (!this.intialized)
      throw new InvalidOperationException("Обработчик не был предварительно инициализирован.");
  }

  internal void BeginCreation()
  {
    this.RequireInitialized();
    using (UserSessionContext.CaptureSession(this.session.SessionGUID))
      this.OnBeginCreation();
  }

  internal void RegisterCreatedObject(IDBObject newObject)
  {
    this.RequireInitialized();
    this.createdObjects.Add(newObject);
  }

  internal void AfterCreateObject(IDBObject newObject, IDBObject prototype)
  {
    this.RequireInitialized();
    using (UserSessionContext.CaptureSession(newObject.Session.SessionGUID))
      this.OnAfterCreateObject(newObject, prototype);
  }

  internal void EndCreation()
  {
    this.RequireInitialized();
    using (UserSessionContext.CaptureSession(this.session.SessionGUID))
      this.OnEndCreation();
  }

  internal void CancelCreation()
  {
    this.RequireInitialized();
    try
    {
      using (UserSessionContext.CaptureSession(this.session.SessionGUID))
        this.OnCancelCreation();
    }
    catch
    {
    }
  }

  protected virtual void OnBeginCreation()
  {
  }

  protected virtual void OnAfterCreateObject(IDBObject newObject, IDBObject prototype)
  {
  }

  protected virtual void OnEndCreation()
  {
  }

  protected virtual void OnCancelCreation()
  {
  }

  protected bool IsVersionAlreadyCreated(long parentVersionId)
  {
    long testId = parentVersionId != 0L && parentVersionId != -1L ? Math.Abs(parentVersionId) : throw new ArgumentException("Не задан идентификатор исходной версии объекта.", nameof (parentVersionId));
    return this.createdObjects.Exists((Predicate<IDBObject>) (item => item.ParentVersionID == testId));
  }

  protected IDBObject FindCreatedVersion(long parentVersionId)
  {
    long testId = parentVersionId != 0L && parentVersionId != -1L ? Math.Abs(parentVersionId) : throw new ArgumentException("Не задан идентификатор исходной версии объекта.", nameof (parentVersionId));
    return this.createdObjects.Find((Predicate<IDBObject>) (item => item.ParentVersionID == testId));
  }

  protected List<IDBObject> GetCreatedObjects()
  {
    return new List<IDBObject>((IEnumerable<IDBObject>) this.createdObjects);
  }

  protected long GetFirstCreatedObjectId() => this.firstCreatedObjectId;
}
