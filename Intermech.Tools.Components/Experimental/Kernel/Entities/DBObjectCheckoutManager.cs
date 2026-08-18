// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.DBObjectCheckoutManager
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Intermech;
using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Tools.Data;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class DBObjectCheckoutManager
{
  private DBModelConfiguration configuration;
  private DBEntityLocalCache entityLocalCache;
  private DBEntityChangeLogBuilder changeLogBuilder;
  private List<DBObjectCheckoutManager.DelayedCheck> delayedChecks;
  private List<DBObjectCheckoutManager.DelayedDBObjectInfo> delayedDBObjectInfos;

  public DBObjectCheckoutManager(
    DBModelConfiguration configuration,
    DBEntityLocalCache entityLocalCache)
  {
    if (configuration == null)
      throw new ArgumentNullException(nameof (configuration));
    if (entityLocalCache == null)
      throw new ArgumentNullException(nameof (entityLocalCache));
    this.configuration = configuration;
    this.entityLocalCache = entityLocalCache;
  }

  private DBModelConfiguration Configuration
  {
    [DebuggerStepThrough] get => this.configuration;
  }

  private DBEntityLocalCache EntityLocalCache
  {
    [DebuggerStepThrough] get => this.entityLocalCache;
  }

  public ICollection<object> FindDBObjects(DBEntityChangeLogBuilder changeLogBuilder)
  {
    if (changeLogBuilder == null)
      throw new ArgumentNullException(nameof (changeLogBuilder));
    try
    {
      this.changeLogBuilder = changeLogBuilder;
      return (ICollection<object>) this.FindDBObjectsInternal();
    }
    finally
    {
      this.changeLogBuilder = (DBEntityChangeLogBuilder) null;
      this.delayedChecks = (List<DBObjectCheckoutManager.DelayedCheck>) null;
      this.delayedDBObjectInfos = (List<DBObjectCheckoutManager.DelayedDBObjectInfo>) null;
    }
  }

  private HashSet<object> FindDBObjectsInternal()
  {
    HashSet<object> dbObjectsInternal = new HashSet<object>();
    this.delayedChecks = new List<DBObjectCheckoutManager.DelayedCheck>(this.changeLogBuilder.ModifiedDBObjects.Count + this.changeLogBuilder.ModifiedDBObjectRelations.Count + this.changeLogBuilder.ModifiedDBRelations.Count);
    this.delayedDBObjectInfos = new List<DBObjectCheckoutManager.DelayedDBObjectInfo>(this.delayedChecks.Capacity * 2);
    foreach (DBEntityChangeLogBuilder.ModifiedDBEntityRecord modifiedDbObject in this.changeLogBuilder.ModifiedDBObjects)
    {
      if (!dbObjectsInternal.Contains(modifiedDbObject.Entity))
        this.DelayCheckingForDBObjectAttributesModifications(modifiedDbObject);
    }
    foreach (DBEntityChangeLogBuilder.ModifiedDBEntityRecord dbObjectRelation in this.changeLogBuilder.ModifiedDBObjectRelations)
    {
      if (!dbObjectsInternal.Contains(dbObjectRelation.Entity))
        this.DelayCheckingForDBObjectRelationsModifications(dbObjectRelation);
    }
    foreach (DBEntityChangeLogBuilder.ModifiedDBEntityRecord modifiedDbRelation in this.changeLogBuilder.ModifiedDBRelations)
    {
      if (!dbObjectsInternal.Contains(modifiedDbRelation.Entity))
        this.DelayCheckingForDBRelationAttributesModifications(modifiedDbRelation);
    }
    this.FetchDBObjectTypes();
    foreach (DBObjectCheckoutManager.DelayedCheck delayedCheck in this.delayedChecks)
    {
      if (!dbObjectsInternal.Contains(delayedCheck.DBObjectEntity) && delayedCheck.Invoke())
        dbObjectsInternal.Add(delayedCheck.DBObjectEntity);
    }
    return dbObjectsInternal;
  }

  private void FetchDBObjectTypes()
  {
    foreach (DBObjectCheckoutManager.DelayedDBObjectInfo delayedDbObjectInfo in this.delayedDBObjectInfos)
    {
      if (delayedDbObjectInfo.DBObjectTypeId == -1 && delayedDbObjectInfo.DBObjectDescriptor.DBObjectType.IsLeafType)
        delayedDbObjectInfo.DBObjectTypeId = delayedDbObjectInfo.DBObjectDescriptor.DBObjectType.Id;
    }
    foreach (DBObjectCheckoutManager.DelayedDBObjectInfo delayedDbObjectInfo in this.delayedDBObjectInfos)
    {
      if (delayedDbObjectInfo.DBObjectTypeId == -1 && delayedDbObjectInfo.DBObjectId == 0L)
        delayedDbObjectInfo.DBObjectTypeId = delayedDbObjectInfo.DBObjectDescriptor.DBObjectType.Id;
    }
    List<long> longList = new List<long>(this.delayedDBObjectInfos.Count);
    for (int index = 0; index < this.delayedDBObjectInfos.Count; ++index)
    {
      DBObjectCheckoutManager.DelayedDBObjectInfo delayedDbObjectInfo = this.delayedDBObjectInfos[index];
      if (delayedDbObjectInfo.DBObjectTypeId == -1 && delayedDbObjectInfo.DBObjectId != 0L)
        longList.Add(delayedDbObjectInfo.DBObjectId);
    }
    if (longList.Count != 0)
    {
      DBRecordSetParams paramSet = new DBRecordSetParams();
      paramSet.RecordCount = -1;
      paramSet.Columns = new object[2]
      {
        (object) ObligatoryObjectAttributes.F_OBJECT_ID,
        (object) ObligatoryObjectAttributes.F_OBJECT_TYPE
      };
      paramSet.Conditions = new ConditionStructure[1]
      {
        new ConditionStructure(-2, RelationalOperators.In, (object) longList.ToArray(), LogicalOperators.NONE, 0, true)
      };
      DataTable dataTable;
      using (SessionKeeper sessionKeeper = new SessionKeeper())
      {
        IDBObjectCollection objectCollection = sessionKeeper.Session.GetObjectCollection(-1);
        objectCollection.LocalTypesMode = true;
        dataTable = objectCollection.Select(paramSet);
      }
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        long dbObjectId = Convert.ToInt64(row[0]);
        int int32 = Convert.ToInt32(row[1]);
        DBObjectCheckoutManager.DelayedDBObjectInfo delayedDbObjectInfo = this.delayedDBObjectInfos.Find((Predicate<DBObjectCheckoutManager.DelayedDBObjectInfo>) (item => item.DBObjectId == dbObjectId));
        if (delayedDbObjectInfo != null)
          delayedDbObjectInfo.DBObjectTypeId = int32;
      }
      if (this.delayedDBObjectInfos.Find((Predicate<DBObjectCheckoutManager.DelayedDBObjectInfo>) (item => !item.IsPopulated)) != null)
        throw new DBUpdateConcurrencyException("Не удалось определить типы измененных объектов IPS, так как количество записей в ответе сервера приложений, не соответствует запрошенному количеству.");
    }
    this.ValidateFetchingDBObjectTypes();
  }

  private void ValidateFetchingDBObjectTypes()
  {
    foreach (DBObjectCheckoutManager.DelayedDBObjectInfo delayedDbObjectInfo in this.delayedDBObjectInfos)
    {
      if (delayedDbObjectInfo.DBObjectTypeId == -1)
        throw new InvalidOperationException($"Для объекта '{delayedDbObjectInfo.DBObjectEntity}' не удалось определить идентификатор типа.");
    }
  }

  private void DelayCheckingForDBObjectAttributesModifications(
    DBEntityChangeLogBuilder.ModifiedDBEntityRecord changeLogRecord)
  {
    object entity = changeLogRecord.Entity;
    IDBObjectEntityTypeDescriptor dbObjectDescriptor = changeLogRecord.EntityTypeDescriptor.AsDBObjectDescriptor();
    if (dbObjectDescriptor.GetKey(entity) < 0L)
      return;
    this.DelayChecking(entity, (Func<bool>) (() =>
    {
      foreach (string modifiedDataProperty in changeLogRecord.ModifiedDataProperties)
      {
        DataPropertyMapping byPropertyName = dbObjectDescriptor.DataPropertiesMappings.GetByPropertyName(modifiedDataProperty, false);
        if (byPropertyName != null && (byPropertyName.IsCheckoutRequired || byPropertyName.IsContent))
          return true;
      }
      return false;
    }));
  }

  private void DelayCheckingForDBObjectRelationsModifications(
    DBEntityChangeLogBuilder.ModifiedDBEntityRecord changeLogRecord)
  {
    object entity = changeLogRecord.Entity;
    IDBObjectEntityTypeDescriptor dbObjectDescriptor1 = changeLogRecord.EntityTypeDescriptor.AsDBObjectDescriptor();
    long key = dbObjectDescriptor1.GetKey(entity);
    if (key < 0L)
      return;
    DBObjectCheckoutManager.DelayedDBObjectInfo parentTypeInfo = this.DelayFetchingDBObjectInfo(entity, dbObjectDescriptor1, key);
    foreach (ModifiedNavigationPropertyRecord navigationProperty in changeLogRecord.ModifiedNavigationProperties)
    {
      DBObjectNavigationPropertyMapping parentPropertyMapping = dbObjectDescriptor1.NavigationPropertiesMappings.GetByPropertyName(navigationProperty.PropertyName, true);
      foreach (NavigationPropertyModification modification in navigationProperty.Modifications)
      {
        IDBEntityTypeDescriptor entityTypeDescriptor = this.Configuration.GetEntityTypeDescriptor(modification.PropertyValue);
        DBEntityKind entityKind = entityTypeDescriptor.EntityKind;
        switch (entityKind)
        {
          case DBEntityKind.Object:
            object propertyValue1 = modification.PropertyValue;
            IDBObjectEntityTypeDescriptor dbObjectDescriptor2 = entityTypeDescriptor.AsDBObjectDescriptor();
            DBObjectCheckoutManager.DelayedDBObjectInfo childTypeInfo1 = this.DelayFetchingDBObjectInfo(propertyValue1, dbObjectDescriptor2, dbObjectDescriptor2.GetKey(propertyValue1));
            this.DelayChecking(entity, (Func<bool>) (() =>
            {
              DBRelationApplicabilityMapping applicabilityMapping = parentPropertyMapping.DBRelationApplicabilities.TryGet(parentTypeInfo.DBObjectTypeId, childTypeInfo1.DBObjectTypeId);
              return applicabilityMapping != null && applicabilityMapping.IsContent;
            }));
            continue;
          case DBEntityKind.Relation:
            object propertyValue2 = modification.PropertyValue;
            object relationEnd = entityTypeDescriptor.AsDBRelationDescriptor().GetRelationEnd(propertyValue2);
            IDBObjectEntityTypeDescriptor dbObjectDescriptor3 = this.Configuration.GetEntityTypeDescriptor(relationEnd).AsDBObjectDescriptor();
            DBObjectCheckoutManager.DelayedDBObjectInfo childTypeInfo2 = this.DelayFetchingDBObjectInfo(relationEnd, dbObjectDescriptor3, dbObjectDescriptor3.GetKey(relationEnd));
            this.DelayChecking(entity, (Func<bool>) (() =>
            {
              DBRelationApplicabilityMapping applicabilityMapping = parentPropertyMapping.DBRelationApplicabilities.TryGet(parentTypeInfo.DBObjectTypeId, childTypeInfo2.DBObjectTypeId);
              return applicabilityMapping != null && applicabilityMapping.IsContent;
            }));
            continue;
          default:
            throw new NotSupportedEnumException((Enum) entityKind);
        }
      }
    }
  }

  private void DelayCheckingForDBRelationAttributesModifications(
    DBEntityChangeLogBuilder.ModifiedDBEntityRecord changeLogRecord)
  {
    object entity = changeLogRecord.Entity;
    IDBRelationEntityTypeDescriptor entityTypeDescriptor = changeLogRecord.EntityTypeDescriptor.AsDBRelationDescriptor();
    ParentEntityPropertyInfo entityPropertyInfo = changeLogRecord.ReferencedBy[0];
    IDBObjectEntityTypeDescriptor dbObjectDescriptor1 = this.Configuration.GetEntityTypeDescriptor(entityPropertyInfo.Entity).AsDBObjectDescriptor();
    long key = dbObjectDescriptor1.GetKey(entityPropertyInfo.Entity);
    if (key < 0L)
      return;
    DBObjectCheckoutManager.DelayedDBObjectInfo parentTypeInfo = this.DelayFetchingDBObjectInfo(entityPropertyInfo.Entity, dbObjectDescriptor1, key);
    DBObjectNavigationPropertyMapping parentPropertyMapping = dbObjectDescriptor1.NavigationPropertiesMappings.GetByPropertyName(entityPropertyInfo.PropertyName, true);
    object relationEnd = entityTypeDescriptor.GetRelationEnd(entity);
    IDBObjectEntityTypeDescriptor dbObjectDescriptor2 = this.Configuration.GetEntityTypeDescriptor(relationEnd).AsDBObjectDescriptor();
    DBObjectCheckoutManager.DelayedDBObjectInfo childTypeInfo = this.DelayFetchingDBObjectInfo(relationEnd, dbObjectDescriptor2, dbObjectDescriptor2.GetKey(relationEnd));
    this.DelayChecking(entityPropertyInfo.Entity, (Func<bool>) (() =>
    {
      DBRelationApplicabilityMapping applicabilityMapping = parentPropertyMapping.DBRelationApplicabilities.TryGet(parentTypeInfo.DBObjectTypeId, childTypeInfo.DBObjectTypeId);
      if (applicabilityMapping == null)
        return false;
      DataPropertyMappings relationAttributes = parentPropertyMapping.DBRelationAttributes;
      foreach (string modifiedDataProperty in changeLogRecord.ModifiedDataProperties)
      {
        DataPropertyMapping byPropertyName = relationAttributes.GetByPropertyName(modifiedDataProperty, false);
        if (byPropertyName != null && (byPropertyName.IsCheckoutRequired || byPropertyName.IsContent) && applicabilityMapping.IsContent)
          return true;
      }
      return false;
    }));
  }

  private DBObjectCheckoutManager.DelayedDBObjectInfo DelayFetchingDBObjectInfo(
    object dbObjectEntity,
    IDBObjectEntityTypeDescriptor dbObjectDescriptor,
    long dbObjectId)
  {
    DBObjectCheckoutManager.DelayedDBObjectInfo delayedDbObjectInfo = this.delayedDBObjectInfos.Find((Predicate<DBObjectCheckoutManager.DelayedDBObjectInfo>) (item => item.DBObjectEntity == dbObjectEntity));
    if (delayedDbObjectInfo == null)
    {
      delayedDbObjectInfo = new DBObjectCheckoutManager.DelayedDBObjectInfo(dbObjectEntity, dbObjectDescriptor, dbObjectId);
      this.delayedDBObjectInfos.Add(delayedDbObjectInfo);
    }
    return delayedDbObjectInfo;
  }

  private void DelayChecking(object dbObjectEntity, Func<bool> predicate)
  {
    this.delayedChecks.Add(new DBObjectCheckoutManager.DelayedCheck(dbObjectEntity, predicate));
  }

  public void CheckoutDBObjects(ICollection<object> dbObjects, IDBEntityBatchUpdateLog updateLog = null)
  {
    if (dbObjects == null)
      throw new ArgumentNullException(nameof (dbObjects));
    List<long> objectList = CollectionUtils.ConvertAsList<object, long>(dbObjects, (Converter<object, long>) (dbObjectEntity => this.Configuration.GetEntityTypeDescriptor(dbObjectEntity).AsDBObjectDescriptor().GetKey(dbObjectEntity)));
    IList<long> longList = DBDocumentHelper.Checkout((IList<long>) objectList, (DBDocumentHelper.CheckoutErrorHandler) null);
    int index = 0;
    List<object> dbObjectsToUpdate = new List<object>(dbObjects.Count);
    foreach (object dbObject in (IEnumerable<object>) dbObjects)
    {
      if (objectList[index] != longList[index])
      {
        IDBObjectEntityTypeDescriptor dbObjectDescriptor = this.Configuration.GetEntityTypeDescriptor(dbObject).AsDBObjectDescriptor();
        dbObjectDescriptor.SetKey(dbObject, longList[index]);
        this.EntityLocalCache.Remove(dbObject, new DBEntityLocalCacheKey(dbObjectDescriptor.EntityType, (object) objectList[index]));
        this.EntityLocalCache.AddOrUpdate(dbObject, new DBEntityLocalCacheKey(dbObjectDescriptor.EntityType, (object) longList[index]));
        if (this.HasAnyNavigationProperties(dbObject, dbObjectDescriptor))
          dbObjectsToUpdate.Add(dbObject);
        updateLog?.CheckoutEntity(dbObject, (object) objectList[index]);
      }
      ++index;
    }
    if (dbObjectsToUpdate.Count == 0)
      return;
    this.UpdateDBRelationIdentifiers((ICollection<object>) dbObjectsToUpdate);
  }

  private bool HasAnyNavigationProperties(
    object dbObjectEntity,
    IDBObjectEntityTypeDescriptor dbObjectDescriptor)
  {
    foreach (DBObjectNavigationPropertyMapping navigationPropertyMapping in (IEnumerable<DBObjectNavigationPropertyMapping>) dbObjectDescriptor.NavigationPropertiesMappings.AsCollection)
    {
      if (navigationPropertyMapping.IsComplex)
      {
        EntityPropertyData entityPropertyData = navigationPropertyMapping.PropertyDescriptor.GetValue(dbObjectEntity);
        if (entityPropertyData.PresenceStatus == EntityMemberPresenceStatus.Present && entityPropertyData.PropertyValue != null)
          return true;
      }
    }
    return false;
  }

  private void UpdateDBRelationIdentifiers(ICollection<object> dbObjectsToUpdate)
  {
    List<DBObjectCheckoutManager.DelayedDBRelationInfo> delayedDbRelationInfoList = new List<DBObjectCheckoutManager.DelayedDBRelationInfo>();
    foreach (object entity in (IEnumerable<object>) dbObjectsToUpdate)
    {
      IDBObjectEntityTypeDescriptor entityTypeDescriptor = this.Configuration.GetEntityTypeDescriptor(entity).AsDBObjectDescriptor();
      long key1 = entityTypeDescriptor.GetKey(entity);
      foreach (DBObjectNavigationPropertyMapping navigationPropertyMapping in (IEnumerable<DBObjectNavigationPropertyMapping>) entityTypeDescriptor.NavigationPropertiesMappings.AsCollection)
      {
        if (navigationPropertyMapping.IsComplex)
        {
          EntityPropertyData entityPropertyData = navigationPropertyMapping.PropertyDescriptor.GetValue(entity);
          IEnumerable<object> objects;
          if (!navigationPropertyMapping.PropertyDescriptor.Definition.IsContainer)
            objects = (IEnumerable<object>) new object[1]
            {
              entityPropertyData.PropertyValue
            };
          else
            objects = (IEnumerable<object>) entityPropertyData.PropertyValue;
          foreach (object obj in objects)
          {
            IDBRelationEntityTypeDescriptor relationDescriptor = this.Configuration.GetEntityTypeDescriptor(obj).AsDBRelationDescriptor();
            long key2 = relationDescriptor.GetKey(obj);
            Guid guid = relationDescriptor.GetGuid(obj);
            if (key2 != 0L && guid != Guid.Empty)
            {
              DBObjectCheckoutManager.DelayedDBRelationInfo delayedDbRelationInfo = new DBObjectCheckoutManager.DelayedDBRelationInfo(key1, obj, relationDescriptor, guid, key2);
              delayedDbRelationInfoList.Add(delayedDbRelationInfo);
            }
          }
        }
      }
    }
    if (delayedDbRelationInfoList.Count == 0)
      return;
    long[] array = CollectionUtils.ToArray<long>((ICollection<long>) new HashSet<long>((IEnumerable<long>) delayedDbRelationInfoList.ConvertAll<long>((Converter<DBObjectCheckoutManager.DelayedDBRelationInfo, long>) (item => item.DBObjectId))));
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Columns = new object[3]
    {
      (object) ObligatoryObjectAttributes.F_PROJ_ID,
      (object) ObligatoryObjectAttributes.F_PRJ_GUID,
      (object) ObligatoryObjectAttributes.F_PRJLINK_ID
    };
    paramSet.Conditions = new ConditionStructure[1]
    {
      new ConditionStructure(-21, RelationalOperators.In, (object) array, LogicalOperators.NONE, 0, true)
    };
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(-1);
      relationCollection.LocalTypesMode = true;
      dataTable = relationCollection.Select(paramSet);
    }
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long projectId = Convert.ToInt64(row[0]);
      Guid relationGuid = new Guid(Convert.ToString(row[1]));
      DBObjectCheckoutManager.DelayedDBRelationInfo delayedDbRelationInfo = delayedDbRelationInfoList.Find((Predicate<DBObjectCheckoutManager.DelayedDBRelationInfo>) (item => item.DBObjectId == projectId && item.RelationGuid == relationGuid));
      if (delayedDbRelationInfo != null)
      {
        long int64 = Convert.ToInt64(row[2]);
        delayedDbRelationInfo.NewRelationId = int64;
        delayedDbRelationInfo.RelationDescriptor.KeyProperty.SetValue(delayedDbRelationInfo.RelationEntity, (object) int64);
      }
    }
    if (delayedDbRelationInfoList.Find((Predicate<DBObjectCheckoutManager.DelayedDBRelationInfo>) (item => !item.IsUpdated)) != null)
      throw new DBUpdateConcurrencyException("Не удалось обновить идентификаторы связей (RelationId) между измененными объектами IPS, так как количество записей в ответе сервера приложений, не соответствует запрошенному количеству.");
  }

  private sealed class DelayedDBObjectInfo
  {
    public DelayedDBObjectInfo(
      object dbObjectEntity,
      IDBObjectEntityTypeDescriptor dbObjectDescriptor,
      long dbObjectId)
    {
      this.DBObjectEntity = dbObjectEntity;
      this.DBObjectDescriptor = dbObjectDescriptor;
      this.DBObjectId = dbObjectId;
      this.DBObjectTypeId = -1;
    }

    public object DBObjectEntity { get; private set; }

    public IDBObjectEntityTypeDescriptor DBObjectDescriptor { get; private set; }

    public long DBObjectId { get; private set; }

    public int DBObjectTypeId { get; set; }

    public bool IsPopulated => this.DBObjectTypeId != -1;
  }

  private sealed class DelayedCheck
  {
    public DelayedCheck(object dbObjectEntity, Func<bool> predicate)
    {
      this.DBObjectEntity = dbObjectEntity;
      this.Predicate = predicate;
    }

    public bool Invoke() => this.Predicate();

    public object DBObjectEntity { get; private set; }

    public Func<bool> Predicate { get; private set; }
  }

  private sealed class DelayedDBRelationInfo
  {
    public DelayedDBRelationInfo(
      long dbObjectId,
      object relationEntity,
      IDBRelationEntityTypeDescriptor relationDescriptor,
      Guid relationGuid,
      long oldRelationId)
    {
      this.DBObjectId = dbObjectId;
      this.RelationEntity = relationEntity;
      this.RelationDescriptor = relationDescriptor;
      this.RelationGuid = relationGuid;
      this.OldRelationId = oldRelationId;
      this.NewRelationId = 0L;
    }

    public long DBObjectId { get; private set; }

    public object RelationEntity { get; private set; }

    public IDBRelationEntityTypeDescriptor RelationDescriptor { get; private set; }

    public Guid RelationGuid { get; private set; }

    public long OldRelationId { get; private set; }

    public long NewRelationId { get; set; }

    public bool IsUpdated => this.NewRelationId != 0L;
  }
}
