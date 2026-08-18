// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBModelRoot
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using System;
using System.Diagnostics;
using System.Reflection;

#nullable disable
namespace Experimental.Kernel.Entities;

public abstract class DBModelRoot : IDBModelRoot
{
  private DBModelConfiguration dataServiceConfiguration;
  private DBEntityReadWriteController readWriteController;
  private IEntityChangeTrackerConfiguration changeTrackerConfiguration;
  private DBEntityLocalCache entityLocalCache;
  private IEntityLocalCache entityLocalCacheFacade;
  private InternalDataService internalDataService;
  private IEntityChangeTracker entityChangeTracker;
  private DBEntityBatchUpdateService batchUpdateService;
  private DBMetadataInfoService metadataInfoService;

  protected DBModelRoot(DBModelConfiguration modelConfiguration)
  {
    this.dataServiceConfiguration = modelConfiguration != null ? modelConfiguration : throw new ArgumentNullException(nameof (modelConfiguration));
    this.readWriteController = new DBEntityReadWriteController();
    this.changeTrackerConfiguration = modelConfiguration.ChangeTrackerConfiguration;
    this.entityLocalCache = new DBEntityLocalCache();
    this.entityLocalCacheFacade = (IEntityLocalCache) new EntityLocalCacheFacade(this.entityLocalCache, this.dataServiceConfiguration);
    this.internalDataService = new InternalDataService(this.dataServiceConfiguration, this.entityLocalCache);
    this.entityChangeTracker = (IEntityChangeTracker) NullEntityChangeTracker.Default;
    this.batchUpdateService = new DBEntityBatchUpdateService(this.internalDataService);
    this.DoCreateDataServices();
  }

  internal DBModelConfiguration Configuration
  {
    [DebuggerStepThrough] get => this.dataServiceConfiguration;
  }

  internal InternalDataService InternalDataService
  {
    [DebuggerStepThrough] get => this.internalDataService;
  }

  protected virtual void DoCreateDataServices()
  {
    foreach (PropertyInfo property in this.GetType().GetProperties())
    {
      if (this.IsEntityDataServiceProperty(property) && property.SetMethod != (MethodInfo) null && property.GetValue((object) this) == null)
      {
        object instance = Activator.CreateInstance(typeof (DBEntityDataService<>).MakeGenericType(property.PropertyType.GenericTypeArguments), (object) this.internalDataService, (object) this.readWriteController);
        property.SetValue((object) this, instance);
      }
    }
  }

  private bool IsEntityDataServiceProperty(PropertyInfo propertyInfo)
  {
    Type propertyType = propertyInfo.PropertyType;
    if (propertyType.IsGenericType)
    {
      foreach (Type type in propertyType.GetInterfaces())
      {
        if (type.GetGenericTypeDefinition() == typeof (IEntityDataService<>))
          return true;
      }
      if (propertyType.GetGenericTypeDefinition() == typeof (IEntityDataService<>))
        return true;
    }
    return false;
  }

  public IEntityLocalCache EntityLocalCache
  {
    [DebuggerStepThrough] get => this.entityLocalCacheFacade;
  }

  public IEntityChangeTracker ChangeTracker
  {
    [DebuggerStepThrough] get => this.entityChangeTracker;
  }

  public IEntityBatchUpdateService BatchUpdateService
  {
    [DebuggerStepThrough] get => (IEntityBatchUpdateService) this.batchUpdateService;
  }

  public DBMetadataInfoService MetadataInfoService
  {
    [DebuggerStepThrough] get
    {
      if (this.metadataInfoService == null)
        this.metadataInfoService = new DBMetadataInfoService(this.dataServiceConfiguration);
      return this.metadataInfoService;
    }
  }

  public IEntityBatchUpdateScope StartBatchUpdate()
  {
    DBEntityChangeTracker changeTracker = new DBEntityChangeTracker(this.changeTrackerConfiguration, this.dataServiceConfiguration);
    foreach (object entity in this.entityLocalCache.GetEntities())
      changeTracker.Attach(entity);
    DBEntityBatchUpdateScope batchUpdateScope = new DBEntityBatchUpdateScope(this, (IEntityBatchUpdateService) this.batchUpdateService, (IEntityChangeTrackerBase) changeTracker);
    this.readWriteController.DisallowAll();
    this.entityChangeTracker = (IEntityChangeTracker) changeTracker;
    return (IEntityBatchUpdateScope) batchUpdateScope;
  }

  internal void StopBatchUpdate()
  {
    this.entityChangeTracker = (IEntityChangeTracker) NullEntityChangeTracker.Default;
    this.readWriteController.AllowAll();
  }
}
