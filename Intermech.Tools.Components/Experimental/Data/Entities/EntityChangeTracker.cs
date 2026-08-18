// Decompiled with JetBrains decompiler
// Type: Experimental.Data.Entities.EntityChangeTracker
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Intermech;
using Intermech.Collections;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics;

#nullable disable
namespace Experimental.Data.Entities;

public abstract class EntityChangeTracker : IEntityChangeTracker, IEntityChangeTrackerBase
{
  private static readonly IList<DataPropertySnapshot> emptyDataProperties = (IList<DataPropertySnapshot>) new ReadOnlyCollection<DataPropertySnapshot>((IList<DataPropertySnapshot>) new DataPropertySnapshot[0]);
  private static readonly IList<NavigationPropertySnapshot> emptyNavigationProperties = (IList<NavigationPropertySnapshot>) new ReadOnlyCollection<NavigationPropertySnapshot>((IList<NavigationPropertySnapshot>) new NavigationPropertySnapshot[0]);
  private static readonly IList<object> unavailableNavigationPropertyContent = (IList<object>) new ReadOnlyCollection<object>((IList<object>) new object[0]);
  private IEntityChangeTrackerConfiguration configuration;
  private HashSet<object> recycleBin;
  private Dictionary<object, EntitySavedInitialState> entityInitialStates;
  private Dictionary<object, EntityCaptureChangesState> entityCurrentStates;
  private EntityChangeTrackerLogBuilder currentChangeLogBuilder;
  private bool isCaptureChangesStarted;

  public EntityChangeTracker(IEntityChangeTrackerConfiguration configuration)
  {
    this.configuration = configuration != null ? configuration : throw new ArgumentNullException(nameof (configuration));
    this.entityInitialStates = new Dictionary<object, EntitySavedInitialState>();
    this.recycleBin = new HashSet<object>();
  }

  /// <summary>Возвращает конфигурацию трекера изменений.</summary>
  public IEntityChangeTrackerConfiguration Configuration
  {
    [DebuggerStepThrough] get => this.configuration;
  }

  public void Attach(object entity)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    this.TryAttachInternal(entity);
  }

  public bool IsAttached(object entity)
  {
    return entity != null ? this.entityInitialStates.ContainsKey(entity) : throw new ArgumentNullException(nameof (entity));
  }

  private bool TryAttachInternal(object entity)
  {
    if (this.entityInitialStates.ContainsKey(entity))
      return false;
    this.DoValidateEntityBeforeAttach(entity);
    List<EntitySavedInitialState> savedInitialStateList = new List<EntitySavedInitialState>();
    savedInitialStateList.Add(this.SaveEntityInitialState(entity));
    for (int index = 0; index < savedInitialStateList.Count; ++index)
    {
      EntitySavedInitialState savedInitialState = savedInitialStateList[index];
      foreach (NavigationPropertySnapshot navigationProperty in (IEnumerable<NavigationPropertySnapshot>) savedInitialState.NavigationProperties)
      {
        string propertyName = navigationProperty.PropertyName;
        ParentEntityPropertyInfo parentInfo = (ParentEntityPropertyInfo) null;
        foreach (object propertyValue in (IEnumerable<object>) navigationProperty.PropertyValues)
        {
          EntitySavedInitialState childRecord = this.TryGetInitialState(propertyValue);
          if (childRecord == null)
          {
            childRecord = this.SaveEntityInitialState(propertyValue);
            savedInitialStateList.Add(childRecord);
          }
          parentInfo = this.UpdateReferencedBy(savedInitialState.Entity, propertyName, (IEntityStateRecord) childRecord, parentInfo);
        }
      }
    }
    return true;
  }

  private ParentEntityPropertyInfo UpdateReferencedBy(
    object parentEntity,
    string parentPropertyName,
    IEntityStateRecord childRecord,
    ParentEntityPropertyInfo parentInfo)
  {
    if (childRecord.ReferencedBy.IsReadOnly)
    {
      childRecord.ReferencedBy = (IList<ParentEntityPropertyInfo>) new List<ParentEntityPropertyInfo>();
      childRecord.IsRootEntity = false;
    }
    if (parentInfo == null)
      parentInfo = new ParentEntityPropertyInfo(parentEntity, parentPropertyName);
    childRecord.ReferencedBy.Add(parentInfo);
    return parentInfo;
  }

  protected virtual void DoValidateEntityBeforeAttach(object entity)
  {
  }

  internal void AttachDataProperty(object entity, string propertyName)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    EntitySavedInitialState initialState = this.TryGetInitialState(entity);
    if (initialState == null)
      throw new InvalidOperationException("TODO:");
    IBasicEntityTypeDescriptor entityDescriptor = this.Configuration.GetEntityDescriptor(entity);
    EntityPropertyDefinition propertyDefinition = entityDescriptor.GetDataPropertyDefinition(propertyName);
    DataPropertySnapshot propertySnapshot = this.TryCaptureEntityDataProperty(entity, entityDescriptor, propertyDefinition);
    if (propertySnapshot == null)
      return;
    int index = CollectionUtils.IndexOf<DataPropertySnapshot>((IEnumerable<DataPropertySnapshot>) initialState.DataProperties, (Predicate<DataPropertySnapshot>) (x => x.PropertyName == propertyName));
    if (index >= 0)
    {
      initialState.DataProperties[index] = propertySnapshot;
    }
    else
    {
      if (initialState.DataProperties.IsReadOnly)
        initialState.DataProperties = (IList<DataPropertySnapshot>) new List<DataPropertySnapshot>();
      initialState.DataProperties.Add(propertySnapshot);
    }
  }

  internal void AttachNavigationProperty(object entity, string propertyName)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    EntitySavedInitialState initialState1 = this.TryGetInitialState(entity);
    if (initialState1 == null)
      throw new InvalidOperationException("TODO:");
    IBasicEntityTypeDescriptor entityDescriptor = this.Configuration.GetEntityDescriptor(entity);
    EntityPropertyDefinition propertyDefinition = entityDescriptor.GetNavigationPropertyDefinition(propertyName);
    NavigationPropertySnapshot propertySnapshot = this.TryCaptureEntityNavigationProperty(entity, entityDescriptor, propertyDefinition);
    int index = CollectionUtils.IndexOf<NavigationPropertySnapshot>((IEnumerable<NavigationPropertySnapshot>) initialState1.NavigationProperties, (Predicate<NavigationPropertySnapshot>) (x => x.PropertyName == propertyName));
    if (index >= 0)
    {
      initialState1.NavigationProperties[index] = propertySnapshot;
    }
    else
    {
      if (initialState1.NavigationProperties.IsReadOnly)
        initialState1.NavigationProperties = (IList<NavigationPropertySnapshot>) new List<NavigationPropertySnapshot>();
      initialState1.NavigationProperties.Add(propertySnapshot);
    }
    ParentEntityPropertyInfo parentInfo = (ParentEntityPropertyInfo) null;
    foreach (object propertyValue in (IEnumerable<object>) propertySnapshot.PropertyValues)
    {
      EntitySavedInitialState initialState2 = this.TryGetInitialState(propertyValue);
      parentInfo = this.UpdateReferencedBy(entity, propertyName, (IEntityStateRecord) initialState2, parentInfo);
    }
  }

  public void MarkToRemove(object entity)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    this.recycleBin.Add(entity);
  }

  public void MarkToRemove(IEnumerable<object> entities)
  {
    if (entities == null)
      throw new ArgumentNullException(nameof (entities));
    foreach (object entity in entities)
    {
      if (entity != null)
        this.recycleBin.Add(entity);
    }
  }

  public ICollection<object> RecycleBin
  {
    [DebuggerStepThrough] get => (ICollection<object>) this.recycleBin;
  }

  protected abstract bool IsNewEntity(object entity);

  private EntitySavedInitialState SaveEntityInitialState(
    object entity,
    bool saveDataProperties = true,
    bool saveNavigationProperties = true)
  {
    IBasicEntityTypeDescriptor entityDescriptor = this.Configuration.GetEntityDescriptor(entity);
    EntitySavedInitialState savedInitialState = new EntitySavedInitialState(entity, true);
    if (saveDataProperties)
      savedInitialState.DataProperties = this.CaptureEntityDataProperties(entity, entityDescriptor);
    if (saveNavigationProperties)
      savedInitialState.NavigationProperties = this.CaptureEntityNavigationProperties(entity, entityDescriptor);
    this.entityInitialStates.Add(entity, savedInitialState);
    return savedInitialState;
  }

  private EntitySavedInitialState TryGetInitialState(object entity)
  {
    EntitySavedInitialState initialState;
    this.entityInitialStates.TryGetValue(entity, out initialState);
    return initialState;
  }

  public List<EntityChangeTrackerLogRecord> GetChangeLog()
  {
    SimpleLogBuilder changeLogBuilder = new SimpleLogBuilder();
    this.CaptureChangesInternal((EntityChangeTrackerLogBuilder) changeLogBuilder);
    return changeLogBuilder.ToChangeLog();
  }

  public void CaptureChanges(EntityChangeTrackerLogBuilder changeLogBuilder)
  {
    if (changeLogBuilder == null)
      throw new ArgumentNullException(nameof (changeLogBuilder));
    this.CaptureChangesInternal(changeLogBuilder);
  }

  private void CaptureChangesInternal(EntityChangeTrackerLogBuilder changeLogBuilder)
  {
    try
    {
      this.currentChangeLogBuilder = changeLogBuilder;
      this.entityCurrentStates = new Dictionary<object, EntityCaptureChangesState>(this.entityInitialStates.Count);
      this.isCaptureChangesStarted = true;
      this.CaptureCurrentStates();
      this.AutoRemoveEntities();
      this.CheckForDanglingReferencesToRemovedEntities();
      this.ScanCurrentStates();
    }
    finally
    {
      this.currentChangeLogBuilder = (EntityChangeTrackerLogBuilder) null;
      this.entityCurrentStates = (Dictionary<object, EntityCaptureChangesState>) null;
      this.isCaptureChangesStarted = false;
    }
  }

  private bool IsCaptureChangesStarted
  {
    [DebuggerStepThrough] get => this.isCaptureChangesStarted;
  }

  [Conditional("DEBUG")]
  private void AssertCaptureChangesIsRunning()
  {
  }

  private void CaptureCurrentStates()
  {
    List<EntityCaptureChangesState> captureChangesStateList = new List<EntityCaptureChangesState>(this.entityInitialStates.Count);
    foreach (KeyValuePair<object, EntitySavedInitialState> entityInitialState in this.entityInitialStates)
    {
      object key = entityInitialState.Key;
      captureChangesStateList.Add(this.CaptureEntityCurrentState(key));
    }
    for (int index = 0; index < captureChangesStateList.Count; ++index)
    {
      EntityCaptureChangesState captureChangesState = captureChangesStateList[index];
      foreach (NavigationPropertySnapshot navigationProperty in (IEnumerable<NavigationPropertySnapshot>) captureChangesState.NavigationProperties)
      {
        string propertyName = navigationProperty.PropertyName;
        ParentEntityPropertyInfo parentInfo = (ParentEntityPropertyInfo) null;
        foreach (object propertyValue in (IEnumerable<object>) navigationProperty.PropertyValues)
        {
          EntityCaptureChangesState childRecord = this.TryGetCurrentState(propertyValue);
          if (childRecord == null)
          {
            this.SaveEntityInitialState(propertyValue, false, false);
            childRecord = this.CaptureEntityCurrentState(propertyValue);
            captureChangesStateList.Add(childRecord);
          }
          parentInfo = this.UpdateReferencedBy(captureChangesState.Entity, propertyName, (IEntityStateRecord) childRecord, parentInfo);
        }
      }
    }
  }

  private IList<DataPropertySnapshot> CaptureEntityDataProperties(
    object entity,
    IBasicEntityTypeDescriptor entityDescriptor)
  {
    ICollection<EntityPropertyDefinition> propertyDefinitions = entityDescriptor.GetDataPropertyDefinitions();
    if (propertyDefinitions.Count == 0)
      return EntityChangeTracker.emptyDataProperties;
    List<DataPropertySnapshot> propertySnapshotList = new List<DataPropertySnapshot>(propertyDefinitions.Count);
    foreach (EntityPropertyDefinition propertyDefinition in (IEnumerable<EntityPropertyDefinition>) propertyDefinitions)
    {
      DataPropertySnapshot propertySnapshot = this.TryCaptureEntityDataProperty(entity, entityDescriptor, propertyDefinition);
      if (propertySnapshot != null)
        propertySnapshotList.Add(propertySnapshot);
    }
    return (IList<DataPropertySnapshot>) propertySnapshotList;
  }

  private DataPropertySnapshot TryCaptureEntityDataProperty(
    object entity,
    IBasicEntityTypeDescriptor entityDescriptor,
    EntityPropertyDefinition propertyDefinition)
  {
    EntityPropertyData dataProperty = entityDescriptor.GetDataProperty(entity, propertyDefinition.Name);
    return dataProperty.PresenceStatus == EntityMemberPresenceStatus.Present ? new DataPropertySnapshot(propertyDefinition.Name, dataProperty.PropertyValue) : (DataPropertySnapshot) null;
  }

  private IList<NavigationPropertySnapshot> CaptureEntityNavigationProperties(
    object entity,
    IBasicEntityTypeDescriptor entityDescriptor)
  {
    ICollection<EntityPropertyDefinition> propertyDefinitions = entityDescriptor.GetNavigationPropertyDefinitions();
    if (propertyDefinitions.Count == 0)
      return EntityChangeTracker.emptyNavigationProperties;
    List<NavigationPropertySnapshot> propertySnapshotList = new List<NavigationPropertySnapshot>(propertyDefinitions.Count);
    foreach (EntityPropertyDefinition propertyDefinition in (IEnumerable<EntityPropertyDefinition>) propertyDefinitions)
      propertySnapshotList.Add(this.TryCaptureEntityNavigationProperty(entity, entityDescriptor, propertyDefinition));
    return (IList<NavigationPropertySnapshot>) propertySnapshotList;
  }

  private NavigationPropertySnapshot TryCaptureEntityNavigationProperty(
    object entity,
    IBasicEntityTypeDescriptor entityDescriptor,
    EntityPropertyDefinition propertyDefinition)
  {
    EntityPropertyData navigationProperty = entityDescriptor.GetNavigationProperty(entity, propertyDefinition.Name);
    ICollection<object> propertyValues;
    switch (navigationProperty.PresenceStatus)
    {
      case EntityMemberPresenceStatus.Present:
        ReadOnlyCollectionWrapper<object> collectionWrapper;
        if (!propertyDefinition.IsContainer)
          collectionWrapper = new ReadOnlyCollectionWrapper<object>((ICollection<object>) new object[1]
          {
            navigationProperty.PropertyValue
          });
        else
          collectionWrapper = new ReadOnlyCollectionWrapper<object>((ICollection<object>) new HashSet<object>((IEnumerable<object>) navigationProperty.PropertyValue));
        propertyValues = (ICollection<object>) collectionWrapper;
        break;
      case EntityMemberPresenceStatus.NotPresent:
        propertyValues = (ICollection<object>) EntityChangeTracker.unavailableNavigationPropertyContent;
        break;
      default:
        throw new NotSupportedEnumException((Enum) navigationProperty.PresenceStatus);
    }
    return new NavigationPropertySnapshot(propertyDefinition.Name, navigationProperty.PresenceStatus, propertyValues);
  }

  private EntityCaptureChangesState CaptureEntityCurrentState(object entity)
  {
    IBasicEntityTypeDescriptor entityDescriptor = this.Configuration.GetEntityDescriptor(entity);
    EntityCaptureChangesState captureChangesState = new EntityCaptureChangesState(entity, true);
    captureChangesState.InitialState = this.TryGetInitialState(entity);
    captureChangesState.NavigationProperties = this.CaptureEntityNavigationProperties(entity, entityDescriptor);
    this.entityCurrentStates.Add(entity, captureChangesState);
    return captureChangesState;
  }

  private EntityCaptureChangesState TryGetCurrentState(object entity)
  {
    EntityCaptureChangesState currentState;
    this.entityCurrentStates.TryGetValue(entity, out currentState);
    return currentState;
  }

  private void AutoRemoveEntities()
  {
    foreach (KeyValuePair<object, EntityCaptureChangesState> entityCurrentState in this.entityCurrentStates)
    {
      object key = entityCurrentState.Key;
      EntityCaptureChangesState captureChangesState = entityCurrentState.Value;
      EntitySavedInitialState initialState = captureChangesState.InitialState;
      if (this.CanAutoRemoveUnreferencedEntity(key, captureChangesState.ReferencedBy, initialState.ReferencedBy))
        this.recycleBin.Add(key);
    }
  }

  protected virtual bool CanAutoRemoveUnreferencedEntity(
    object entity,
    IList<ParentEntityPropertyInfo> referencedBy,
    IList<ParentEntityPropertyInfo> initiallyReferencedBy)
  {
    return false;
  }

  private void CheckForDanglingReferencesToRemovedEntities()
  {
    foreach (object entity in this.recycleBin)
    {
      EntityCaptureChangesState currentState = this.TryGetCurrentState(entity);
      if (currentState != null && currentState.ReferencedBy.Count != 0)
      {
        foreach (ParentEntityPropertyInfo entityPropertyInfo in (IEnumerable<ParentEntityPropertyInfo>) currentState.ReferencedBy)
        {
          if (!this.recycleBin.Contains(entityPropertyInfo.Entity))
            throw new EntityValidationException(entity, $"Невозможно удалить доменный объект '{entity}', так как на него есть ссылки из других доменных объектов.");
        }
      }
    }
  }

  private void ScanCurrentStates()
  {
    foreach (KeyValuePair<object, EntityCaptureChangesState> entityCurrentState1 in this.entityCurrentStates)
    {
      object key = entityCurrentState1.Key;
      EntityCaptureChangesState entityCurrentState2 = entityCurrentState1.Value;
      EntitySavedInitialState initialState = entityCurrentState2.InitialState;
      this.DoValidateEntityBeforeScan(key, entityCurrentState2.ReferencedBy, initialState.ReferencedBy);
      if (this.recycleBin.Contains(key))
      {
        if (!this.IsNewEntity(key))
          this.AnalyzeRemovedEntity(initialState);
      }
      else if (this.IsNewEntity(key))
        this.AnalyzeNewEntity(entityCurrentState2);
      else
        this.AnalyzeExistingEntity(entityCurrentState2);
    }
  }

  protected virtual void DoValidateEntityBeforeScan(
    object entity,
    IList<ParentEntityPropertyInfo> referencedBy,
    IList<ParentEntityPropertyInfo> initiallyReferencedBy)
  {
  }

  private void AnalyzeNewEntity(EntityCaptureChangesState entityCurrentState)
  {
    CreatedEntityRecord createdEntityRecord = this.currentChangeLogBuilder.CreateCreatedEntityRecord(entityCurrentState.Entity, entityCurrentState.IsRootEntity);
    if (entityCurrentState.ReferencedBy.Count != 0)
      createdEntityRecord.ReferencedBy.AddRange((IEnumerable<ParentEntityPropertyInfo>) entityCurrentState.ReferencedBy);
    foreach (NavigationPropertySnapshot navigationProperty in (IEnumerable<NavigationPropertySnapshot>) entityCurrentState.NavigationProperties)
    {
      if (navigationProperty.PresenceStatus == EntityMemberPresenceStatus.Present && navigationProperty.PropertyValues.Count != 0)
      {
        ModifiedNavigationPropertyRecord navigationPropertyRecord = new ModifiedNavigationPropertyRecord(navigationProperty.PropertyName);
        foreach (object propertyValue in (IEnumerable<object>) navigationProperty.PropertyValues)
          navigationPropertyRecord.Modifications.Add(this.currentChangeLogBuilder.CreateNavigationPropertyModification(navigationProperty.PropertyName, NavigationPropertyModificationType.Added, propertyValue));
        createdEntityRecord.ModifiedNavigationProperties.Add(navigationPropertyRecord);
      }
    }
    this.currentChangeLogBuilder.Add(createdEntityRecord);
  }

  private void AnalyzeExistingEntity(EntityCaptureChangesState entityCurrentState)
  {
    IBasicEntityTypeDescriptor entityDescriptor = this.configuration.GetEntityDescriptor(entityCurrentState.Entity);
    ICollection<EntityPropertyDefinition> propertyDefinitions = entityDescriptor.GetDataPropertyDefinitions();
    List<string> collection1 = new List<string>();
    foreach (EntityPropertyDefinition propertyDefinition1 in (IEnumerable<EntityPropertyDefinition>) propertyDefinitions)
    {
      EntityPropertyDefinition propertyDefinition = propertyDefinition1;
      EntityPropertyData dataProperty = entityDescriptor.GetDataProperty(entityCurrentState.Entity, propertyDefinition.Name);
      DataPropertySnapshot propertySnapshot = CollectionUtils.Find<DataPropertySnapshot>((IEnumerable<DataPropertySnapshot>) entityCurrentState.InitialState.DataProperties, (Predicate<DataPropertySnapshot>) (item => item.PropertyName == propertyDefinition.Name));
      if (propertySnapshot != null && !object.Equals(dataProperty.PropertyValue, propertySnapshot.PropertyValue))
        collection1.Add(propertyDefinition.Name);
    }
    List<ModifiedNavigationPropertyRecord> collection2 = new List<ModifiedNavigationPropertyRecord>();
    foreach (NavigationPropertySnapshot navigationProperty in (IEnumerable<NavigationPropertySnapshot>) entityCurrentState.NavigationProperties)
    {
      NavigationPropertySnapshot currentPropertySnapshot = navigationProperty;
      if (currentPropertySnapshot.PresenceStatus == EntityMemberPresenceStatus.Present)
      {
        NavigationPropertySnapshot savedState = CollectionUtils.Find<NavigationPropertySnapshot>((IEnumerable<NavigationPropertySnapshot>) entityCurrentState.InitialState.NavigationProperties, (Predicate<NavigationPropertySnapshot>) (item => item.PropertyName == currentPropertySnapshot.PropertyName));
        if (savedState != null)
        {
          ModifiedNavigationPropertyRecord navigationPropertyRecord = this.CompareCollections(entityCurrentState, entityDescriptor, currentPropertySnapshot, savedState);
          if (navigationPropertyRecord.Modifications.Count != 0)
            collection2.Add(navigationPropertyRecord);
        }
      }
    }
    if (collection1.Count != 0 || collection2.Count != 0)
    {
      ModifiedEntityRecord modifiedEntityRecord = this.currentChangeLogBuilder.CreateModifiedEntityRecord(entityCurrentState.Entity, entityCurrentState.IsRootEntity);
      if (entityCurrentState.ReferencedBy.Count != 0)
        modifiedEntityRecord.ReferencedBy.AddRange((IEnumerable<ParentEntityPropertyInfo>) entityCurrentState.ReferencedBy);
      if (collection1.Count != 0)
        modifiedEntityRecord.ModifiedDataProperties.AddRange((IEnumerable<string>) collection1);
      if (collection2.Count != 0)
        modifiedEntityRecord.ModifiedNavigationProperties.AddRange((IEnumerable<ModifiedNavigationPropertyRecord>) collection2);
      this.currentChangeLogBuilder.Add(modifiedEntityRecord);
    }
    else
    {
      if (!this.currentChangeLogBuilder.CanHandleUnmodifiedEntities)
        return;
      UnmodifiedEntityRecord unmodifiedEntityRecord = this.currentChangeLogBuilder.CreateUnmodifiedEntityRecord(entityCurrentState.Entity, entityCurrentState.IsRootEntity);
      if (entityCurrentState.ReferencedBy.Count != 0)
        unmodifiedEntityRecord.ReferencedBy.AddRange((IEnumerable<ParentEntityPropertyInfo>) entityCurrentState.ReferencedBy);
      this.currentChangeLogBuilder.Add(unmodifiedEntityRecord);
    }
  }

  private void AnalyzeRemovedEntity(EntitySavedInitialState entityInitialState)
  {
    RemovedEntityRecord removedEntityRecord = this.currentChangeLogBuilder.CreateRemovedEntityRecord(entityInitialState.Entity, entityInitialState.IsRootEntity);
    if (entityInitialState.ReferencedBy.Count != 0)
      removedEntityRecord.InitiallyReferencedBy.AddRange((IEnumerable<ParentEntityPropertyInfo>) entityInitialState.ReferencedBy);
    foreach (NavigationPropertySnapshot navigationProperty in (IEnumerable<NavigationPropertySnapshot>) entityInitialState.NavigationProperties)
    {
      if (navigationProperty.PresenceStatus == EntityMemberPresenceStatus.Present && navigationProperty.PropertyValues.Count != 0)
      {
        ModifiedNavigationPropertyRecord navigationPropertyRecord = new ModifiedNavigationPropertyRecord(navigationProperty.PropertyName);
        foreach (object propertyValue in (IEnumerable<object>) navigationProperty.PropertyValues)
          navigationPropertyRecord.Modifications.Add(this.currentChangeLogBuilder.CreateNavigationPropertyModification(navigationProperty.PropertyName, NavigationPropertyModificationType.Removed, propertyValue));
        removedEntityRecord.ModifiedNavigationProperties.Add(navigationPropertyRecord);
      }
    }
    this.currentChangeLogBuilder.Add(removedEntityRecord);
  }

  private ModifiedNavigationPropertyRecord CompareCollections(
    EntityCaptureChangesState entityCurrentState,
    IBasicEntityTypeDescriptor entityDescriptor,
    NavigationPropertySnapshot currentState,
    NavigationPropertySnapshot savedState)
  {
    ModifiedNavigationPropertyRecord navigationPropertyRecord = new ModifiedNavigationPropertyRecord(currentState.PropertyName);
    foreach (object propertyValue in (IEnumerable<object>) currentState.PropertyValues)
    {
      switch (savedState.PresenceStatus)
      {
        case EntityMemberPresenceStatus.Present:
          if (!savedState.PropertyValues.Contains(propertyValue))
          {
            navigationPropertyRecord.Modifications.Add(this.currentChangeLogBuilder.CreateNavigationPropertyModification(currentState.PropertyName, NavigationPropertyModificationType.Added, propertyValue));
            continue;
          }
          continue;
        case EntityMemberPresenceStatus.NotPresent:
          navigationPropertyRecord.Modifications.Add(this.currentChangeLogBuilder.CreateNavigationPropertyModification(currentState.PropertyName, NavigationPropertyModificationType.Added, propertyValue));
          continue;
        default:
          continue;
      }
    }
    if (savedState.PresenceStatus == EntityMemberPresenceStatus.Present)
    {
      foreach (object propertyValue in (IEnumerable<object>) savedState.PropertyValues)
      {
        if (!currentState.PropertyValues.Contains(propertyValue))
          navigationPropertyRecord.Modifications.Add(this.currentChangeLogBuilder.CreateNavigationPropertyModification(currentState.PropertyName, NavigationPropertyModificationType.Removed, propertyValue));
      }
    }
    return navigationPropertyRecord;
  }
}
