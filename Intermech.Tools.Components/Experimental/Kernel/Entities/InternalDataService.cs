// Decompiled with JetBrains decompiler
// Type: Experimental.Kernel.Entities.InternalDataService
// Assembly: Intermech.Tools.Components, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B3B1E810-E6FF-4FCB-865A-C312BFC44AFF
// Assembly location: D:\IPS\Client\Intermech.Tools.Components.dll
// XML documentation location: D:\IPS\Client\Intermech.Tools.Components.xml

using Experimental.Data.Entities;
using Intermech;
using Intermech.Collections;
using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;
using System.IO;

#nullable disable
namespace Experimental.Kernel.Entities;

internal sealed class InternalDataService
{
  private DBModelConfiguration configuration;
  private DBEntityLocalCache entityLocalCache;
  private DataPropertyHelper dataPropertyHelper;

  public InternalDataService(
    DBModelConfiguration configuration,
    DBEntityLocalCache entityLocalCache)
  {
    if (configuration == null)
      throw new ArgumentNullException(nameof (configuration));
    if (entityLocalCache == null)
      throw new ArgumentNullException(nameof (entityLocalCache));
    this.configuration = configuration;
    this.entityLocalCache = entityLocalCache;
    this.dataPropertyHelper = DataPropertyHelper.DefaultInstance;
  }

  public DBEntityLocalCache EntityLocalCache
  {
    [DebuggerStepThrough] get => this.entityLocalCache;
  }

  public DBModelConfiguration Configuration
  {
    [DebuggerStepThrough] get => this.configuration;
  }

  public object Load(long entityKey, IDBObjectEntityTypeDescriptor entityTypeDescriptor)
  {
    this.CheckEntityKeyIsDefined(entityKey, entityTypeDescriptor);
    DBEntityLocalCacheKey key = new DBEntityLocalCacheKey(entityTypeDescriptor.EntityType, (object) entityKey);
    object entityFromDbObject = this.EntityLocalCache.TryGet(key);
    if (entityFromDbObject == null)
    {
      entityFromDbObject = this.CreateEntityFromDBObject(entityKey, entityTypeDescriptor);
      this.EntityLocalCache.AddOrUpdate(entityFromDbObject, key);
    }
    return entityFromDbObject;
  }

  private object CreateEntityFromDBObject(
    long entityKey,
    IDBObjectEntityTypeDescriptor entityTypeDescriptor)
  {
    ICollection<DataPropertyMapping> asCollection = entityTypeDescriptor.DataPropertiesMappings.AsCollection;
    AttributeValues[] entityAttributes = this.LoadEntityAttributeValues(entityKey, asCollection);
    return this.CreateEntityFromAttributeValues(entityTypeDescriptor, asCollection, entityAttributes);
  }

  public List<TEntity> LoadAll<TEntity>(ConditionStructure[] conditions = null)
  {
    IDBObjectEntityTypeDescriptor entityTypeDescriptor = this.Configuration.GetEntityTypeDescriptor(typeof (TEntity)).AsDBObjectDescriptor();
    ICollection<DataPropertyMapping> asCollection = entityTypeDescriptor.DataPropertiesMappings.AsCollection;
    DataPropertyMapping entityKeyPropertyMapping = entityTypeDescriptor.DataPropertiesMappings.GetByPropertyName(entityTypeDescriptor.KeyProperty.Definition.Name, true);
    int columnIndex = CollectionUtils.IndexOf<DataPropertyMapping>((IEnumerable<DataPropertyMapping>) asCollection, (Predicate<DataPropertyMapping>) (x => x.Id == entityKeyPropertyMapping.Id));
    DataTable dataTable = this.LoadEntitiesAttributeTable(entityTypeDescriptor, asCollection, conditions);
    List<TEntity> entityList = new List<TEntity>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      long propertyValue = (long) this.ConvertToPropertyValue(entityKeyPropertyMapping, row, columnIndex);
      DBEntityLocalCacheKey key = new DBEntityLocalCacheKey(entityTypeDescriptor.EntityType, (object) propertyValue);
      object entityFromDataRow = this.EntityLocalCache.TryGet(key);
      if (entityFromDataRow == null)
      {
        entityFromDataRow = this.CreateEntityFromDataRow(entityTypeDescriptor, asCollection, row);
        this.EntityLocalCache.AddOrUpdate(entityFromDataRow, key);
      }
      entityList.Add((TEntity) entityFromDataRow);
    }
    return entityList;
  }

  public void LoadReferences(object entity, string propertyName)
  {
    if (entity == null)
      throw new ArgumentNullException(nameof (entity));
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    DBObjectNavigationPropertyMapping byPropertyName = this.Configuration.GetEntityTypeDescriptor(entity).AsDBObjectDescriptor().NavigationPropertiesMappings.GetByPropertyName(propertyName, true);
    DBEntityKind entityKind = this.Configuration.GetEntityTypeDescriptor(byPropertyName.PropertyDescriptor.Definition.ContainerItemType).EntityKind;
    switch (entityKind)
    {
      case DBEntityKind.Object:
        List<object> collection1 = this.LoadChildEntities(entity, propertyName, byPropertyName.PropertyDescriptor.Definition.ContainerItemType);
        byPropertyName.PropertyDescriptor.ValueWriter.AssignValueFromCollection(entity, (IEnumerable<object>) collection1);
        break;
      case DBEntityKind.Relation:
        List<object> collection2 = this.LoadChildOccurences(entity, propertyName, byPropertyName.PropertyDescriptor.Definition.ContainerItemType);
        byPropertyName.PropertyDescriptor.ValueWriter.AssignValueFromCollection(entity, (IEnumerable<object>) collection2);
        break;
      default:
        throw new NotSupportedEnumException((Enum) entityKind);
    }
  }

  private List<object> LoadChildEntities(object parentEntity, string propertyName, Type childType)
  {
    IDBObjectEntityTypeDescriptor parentDescriptor = this.Configuration.GetEntityTypeDescriptor(parentEntity).AsDBObjectDescriptor();
    DBObjectNavigationPropertyMapping byPropertyName = parentDescriptor.NavigationPropertiesMappings.GetByPropertyName(propertyName, true);
    IDBObjectEntityTypeDescriptor entityTypeDescriptor = this.Configuration.GetEntityTypeDescriptor(childType).AsDBObjectDescriptor();
    DataTable dataTable = this.LoadChildEntitiesKeyTable(parentEntity, parentDescriptor, byPropertyName, entityTypeDescriptor);
    List<object> objectList = new List<object>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      object obj = this.Load(Convert.ToInt64(row[0]), entityTypeDescriptor);
      objectList.Add(obj);
    }
    return objectList;
  }

  private List<object> LoadChildOccurences(
    object parentEntity,
    string propertyName,
    Type occurenceType)
  {
    IDBObjectEntityTypeDescriptor parentDescriptor = this.Configuration.GetEntityTypeDescriptor(parentEntity).AsDBObjectDescriptor();
    DBObjectNavigationPropertyMapping byPropertyName = parentDescriptor.NavigationPropertiesMappings.GetByPropertyName(propertyName, true);
    IDBRelationEntityTypeDescriptor childOccurenceDescriptor = this.Configuration.GetEntityTypeDescriptor(occurenceType).AsDBRelationDescriptor();
    ICollection<DataPropertyMapping> asCollection = byPropertyName.DBRelationAttributes.AsCollection;
    IDBObjectEntityTypeDescriptor entityTypeDescriptor = this.Configuration.GetEntityTypeDescriptor(childOccurenceDescriptor.RelationEndProperty.Definition.PropertyType).AsDBObjectDescriptor();
    (DataTable dataTable, int columnIndex) = this.LoadChildEntitiesAttributeTable(parentEntity, parentDescriptor, byPropertyName, entityTypeDescriptor, asCollection);
    List<object> objectList = new List<object>(dataTable.Rows.Count);
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      object childEntity = this.Load(Convert.ToInt64(row[columnIndex]), entityTypeDescriptor);
      object occurenceFromDataRow = this.CreateChildOccurenceFromDataRow(parentEntity, childEntity, childOccurenceDescriptor, asCollection, row);
      objectList.Add(occurenceFromDataRow);
    }
    return objectList;
  }

  private GetAttributeValuesModes CalculateGetAttributeValuesMode(
    ICollection<DataPropertyMapping> dataPropertyMappings)
  {
    GetAttributeValuesModes attributeValuesMode = GetAttributeValuesModes.None;
    foreach (DataPropertyMapping dataPropertyMapping in (IEnumerable<DataPropertyMapping>) dataPropertyMappings)
      attributeValuesMode |= dataPropertyMapping.ValueLoadParameters.KeyEntityLoadMode;
    return attributeValuesMode;
  }

  private AttributeValues[] LoadEntityAttributeValues(
    long entityKey,
    ICollection<DataPropertyMapping> dataPropertyMappings)
  {
    GetAttributeValuesModes attributeValuesMode = this.CalculateGetAttributeValuesMode(dataPropertyMappings);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObject(entityKey, true).GetAttributesValues(attributeValuesMode);
  }

  private DataTable LoadEntitiesAttributeTable(
    IDBObjectEntityTypeDescriptor entityTypeDescriptor,
    ICollection<DataPropertyMapping> dataPropertyMappings,
    ConditionStructure[] conditions = null)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Columns = new object[dataPropertyMappings.Count];
    paramSet.ColumnsInfo = new ColumnInfo[dataPropertyMappings.Count];
    paramSet.Contents = new ColumnContents[dataPropertyMappings.Count];
    int index = 0;
    foreach (DataPropertyMapping dataPropertyMapping in (IEnumerable<DataPropertyMapping>) dataPropertyMappings)
    {
      paramSet.Columns[index] = (object) dataPropertyMapping.Id;
      paramSet.ColumnsInfo[index] = new ColumnInfo((object) dataPropertyMapping.Id, AttributeSourceTypes.Object, (object) null);
      paramSet.Contents[index] = dataPropertyMapping.ValueLoadParameters.BatchLoadMode;
      ++index;
    }
    if (conditions != null)
      paramSet.Conditions = conditions;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      return sessionKeeper.Session.GetObjectCollection(entityTypeDescriptor.DBObjectType.Id).Select(paramSet);
  }

  private DataTable LoadChildEntitiesKeyTable(
    object parentEntity,
    IDBObjectEntityTypeDescriptor parentDescriptor,
    DBObjectNavigationPropertyMapping navigationPropertyMapping,
    IDBObjectEntityTypeDescriptor childDescriptor)
  {
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Columns = new object[1]
    {
      (object) ObligatoryObjectAttributes.F_OBJECT_ID
    };
    paramSet.ColumnsInfo = new ColumnInfo[1]
    {
      new ColumnInfo((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, (object) null)
    };
    long key = parentDescriptor.GetKey(parentEntity);
    VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(navigationPropertyMapping.DBRelationType.Id);
      relationCollection.FiltrationOwnerID = editorRule.OwnerId;
      if (childDescriptor.DBObjectType.IsLocalType)
        relationCollection.ObjectTypeID = childDescriptor.DBObjectType.Id;
      return relationCollection.ConsistFrom(paramSet, key);
    }
  }

  private (DataTable, int) LoadChildEntitiesAttributeTable(
    object parentEntity,
    IDBObjectEntityTypeDescriptor parentDescriptor,
    DBObjectNavigationPropertyMapping navigationPropertyMapping,
    IDBObjectEntityTypeDescriptor childDescriptor,
    ICollection<DataPropertyMapping> childOccurenceProperties)
  {
    int length = childOccurenceProperties.Count + 1;
    DBRecordSetParams paramSet = new DBRecordSetParams();
    paramSet.RecordCount = -1;
    paramSet.Columns = new object[length];
    paramSet.ColumnsInfo = new ColumnInfo[length];
    paramSet.Contents = new ColumnContents[length];
    int index1 = 0;
    foreach (DataPropertyMapping occurenceProperty in (IEnumerable<DataPropertyMapping>) childOccurenceProperties)
    {
      paramSet.Columns[index1] = (object) occurenceProperty.Id;
      paramSet.ColumnsInfo[index1] = new ColumnInfo((object) occurenceProperty.Id, AttributeSourceTypes.Relation, (object) null);
      paramSet.Contents[index1] = occurenceProperty.ValueLoadParameters.BatchLoadMode;
      ++index1;
    }
    int num1 = index1;
    int num2 = num1 + 1;
    int index2 = num1;
    paramSet.Columns[index2] = (object) ObligatoryObjectAttributes.F_OBJECT_ID;
    paramSet.ColumnsInfo[index2] = new ColumnInfo((object) ObligatoryObjectAttributes.F_OBJECT_ID, AttributeSourceTypes.Object, (object) null);
    paramSet.Contents[index2] = ColumnContents.Text;
    long key = parentDescriptor.GetKey(parentEntity);
    VersionsRulePackage editorRule = VersionsRuleSources.GetEditorRule();
    DataTable dataTable;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelationCollection relationCollection = sessionKeeper.Session.GetRelationCollection(navigationPropertyMapping.DBRelationType.Id);
      relationCollection.FiltrationOwnerID = editorRule.OwnerId;
      if (childDescriptor.DBObjectType.IsLocalType)
        relationCollection.ObjectTypeID = childDescriptor.DBObjectType.Id;
      dataTable = relationCollection.ConsistFrom(paramSet, key);
    }
    return (dataTable, index2);
  }

  private object CreateEntityFromAttributeValues(
    IDBObjectEntityTypeDescriptor entityTypeDescriptor,
    ICollection<DataPropertyMapping> dataPropertyMappings,
    AttributeValues[] entityAttributes)
  {
    object instance = entityTypeDescriptor.CreateInstance();
    foreach (DataPropertyMapping dataPropertyMapping in (IEnumerable<DataPropertyMapping>) dataPropertyMappings)
    {
      DataPropertyMapping propertyMapping = dataPropertyMapping;
      AttributeValues dbAttributeData = CollectionUtils.Find<AttributeValues>((IEnumerable<AttributeValues>) entityAttributes, (Predicate<AttributeValues>) (x => x.AttributeID == propertyMapping.Id));
      object propertyValue = this.ConvertToPropertyValue(propertyMapping, dbAttributeData);
      this.SetEntityPropertyValue((IDBEntityTypeDescriptor) entityTypeDescriptor, instance, propertyMapping, propertyValue);
    }
    return instance;
  }

  private object CreateEntityFromDataRow(
    IDBObjectEntityTypeDescriptor entityTypeDescriptor,
    ICollection<DataPropertyMapping> dataPropertyMappings,
    DataRow row)
  {
    object instance = entityTypeDescriptor.CreateInstance();
    int columnIndex = 0;
    foreach (DataPropertyMapping dataPropertyMapping in (IEnumerable<DataPropertyMapping>) dataPropertyMappings)
    {
      object propertyValue = this.ConvertToPropertyValue(dataPropertyMapping, row, columnIndex);
      this.SetEntityPropertyValue((IDBEntityTypeDescriptor) entityTypeDescriptor, instance, dataPropertyMapping, propertyValue);
      ++columnIndex;
    }
    return instance;
  }

  private object CreateChildOccurenceFromDataRow(
    object parentEntity,
    object childEntity,
    IDBRelationEntityTypeDescriptor childOccurenceDescriptor,
    ICollection<DataPropertyMapping> childOccurenceDataProperties,
    DataRow row)
  {
    object instance = childOccurenceDescriptor.CreateInstance(parentEntity, childEntity);
    int columnIndex = 0;
    foreach (DataPropertyMapping occurenceDataProperty in (IEnumerable<DataPropertyMapping>) childOccurenceDataProperties)
    {
      object propertyValue = this.ConvertToPropertyValue(occurenceDataProperty, row, columnIndex);
      this.SetEntityPropertyValue((IDBEntityTypeDescriptor) childOccurenceDescriptor, instance, occurenceDataProperty, propertyValue);
      ++columnIndex;
    }
    return instance;
  }

  public void CreateBlankDBObject(
    object dbObjectEntity,
    IDBObjectEntityTypeDescriptor dbObjectDescriptor)
  {
    long newKey = this.GetEmptyEntityKey(dbObjectEntity, dbObjectDescriptor);
    InternalDataService.ModifiedAttributes modifiedAttributes = this.ConvertToModifiedAttributes(dbObjectEntity, (IDBEntityTypeDescriptor) dbObjectDescriptor, dbObjectDescriptor.DataPropertiesMappings);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObjectCollection(dbObjectDescriptor.DBObjectType.Id).Create();
      if (modifiedAttributes.SimpleAttributes.Length != 0)
        dbObject.SetAttributesValues(modifiedAttributes.SimpleAttributes);
      if (modifiedAttributes.FileAttributes.Count != 0)
        this.UpdateFileAttributes(dbObjectEntity, (IDBAttributable) dbObject, modifiedAttributes.FileAttributes);
      if (modifiedAttributes.RemovedAttributes.Count != 0)
        this.RemoveAttributes((IDBAttributable) dbObject, modifiedAttributes.RemovedAttributes);
      newKey = dbObject.ObjectID;
    }
    dbObjectDescriptor.SetKey(dbObjectEntity, newKey);
  }

  public void CommitBlankDBObject(
    object dbObjectEntity,
    IDBObjectEntityTypeDescriptor dbObjectDescriptor)
  {
    long definedEntityKey = this.GetDefinedEntityKey(dbObjectEntity, dbObjectDescriptor);
    long objectId;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(definedEntityKey, true);
      dbObject.CommitCreation(true, true);
      objectId = dbObject.ObjectID;
    }
    if (objectId != definedEntityKey)
      dbObjectDescriptor.SetKey(dbObjectEntity, definedEntityKey);
    this.EntityLocalCache.AddOrUpdate(dbObjectEntity, new DBEntityLocalCacheKey(dbObjectDescriptor.EntityType, (object) definedEntityKey));
  }

  public bool UpdateDBObjectAttributes(
    object dbObjectEntity,
    IDBObjectEntityTypeDescriptor dbObjectDescriptor,
    ICollection<string> modifiedProperties)
  {
    long definedEntityKey = this.GetDefinedEntityKey(dbObjectEntity, dbObjectDescriptor);
    if (modifiedProperties.Count == 0)
      return false;
    InternalDataService.ModifiedAttributes modifiedAttributes = this.ConvertToModifiedAttributes(dbObjectEntity, (IDBEntityTypeDescriptor) dbObjectDescriptor, dbObjectDescriptor.DataPropertiesMappings, modifiedProperties);
    if (modifiedAttributes.IsEmpty())
      return false;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBObject dbObject = sessionKeeper.Session.GetObject(definedEntityKey, true);
      if (modifiedAttributes.SimpleAttributes.Length != 0)
        dbObject.SetAttributesValues(modifiedAttributes.SimpleAttributes);
      if (modifiedAttributes.FileAttributes.Count != 0)
        this.UpdateFileAttributes(dbObjectEntity, (IDBAttributable) dbObject, modifiedAttributes.FileAttributes);
      if (modifiedAttributes.RemovedAttributes.Count != 0)
        this.RemoveAttributes((IDBAttributable) dbObject, modifiedAttributes.RemovedAttributes);
    }
    return true;
  }

  public void RemoveDBObject(
    object dbObjectEntity,
    IDBObjectEntityTypeDescriptor dbObjectDescriptor)
  {
    long definedEntityKey = this.GetDefinedEntityKey(dbObjectEntity, dbObjectDescriptor);
    long objectID = definedEntityKey;
    if (objectID < 0L)
      objectID = -objectID;
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetObject(objectID, true).Delete(0L);
    this.EntityLocalCache.Remove(dbObjectEntity, new DBEntityLocalCacheKey(dbObjectDescriptor.EntityType, (object) definedEntityKey));
  }

  public void CreateSimpleDBRelation(object parentEntity, string propertyName, object childEntity)
  {
    if (parentEntity == null)
      throw new ArgumentNullException(nameof (parentEntity));
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    if (childEntity == null)
      throw new ArgumentNullException(nameof (childEntity));
    IDBObjectEntityTypeDescriptor entityTypeDescriptor = this.Configuration.GetEntityTypeDescriptor(parentEntity).AsDBObjectDescriptor();
    DBObjectNavigationPropertyMapping byPropertyName = entityTypeDescriptor.NavigationPropertiesMappings.GetByPropertyName(propertyName, true);
    long key1 = entityTypeDescriptor.GetKey(parentEntity);
    long key2 = this.Configuration.GetEntityTypeDescriptor(childEntity).AsDBObjectDescriptor().GetKey(childEntity);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetRelationCollection(byPropertyName.DBRelationType.Id).Create(key1, key2);
  }

  public void CreateComplexDBRelation(
    object parentEntity,
    string propertyName,
    object childOccurence)
  {
    if (parentEntity == null)
      throw new ArgumentNullException(nameof (parentEntity));
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    if (childOccurence == null)
      throw new ArgumentNullException(nameof (childOccurence));
    IDBObjectEntityTypeDescriptor entityTypeDescriptor1 = this.Configuration.GetEntityTypeDescriptor(parentEntity).AsDBObjectDescriptor();
    DBObjectNavigationPropertyMapping byPropertyName = entityTypeDescriptor1.NavigationPropertiesMappings.GetByPropertyName(propertyName, true);
    long key1 = entityTypeDescriptor1.GetKey(parentEntity);
    IDBRelationEntityTypeDescriptor entityTypeDescriptor2 = this.Configuration.GetEntityTypeDescriptor(childOccurence).AsDBRelationDescriptor();
    object relationEnd = entityTypeDescriptor2.GetRelationEnd(childOccurence);
    long key2 = this.Configuration.GetEntityTypeDescriptor(relationEnd).AsDBObjectDescriptor().GetKey(relationEnd);
    InternalDataService.ModifiedAttributes modifiedAttributes = this.ConvertToModifiedAttributes(childOccurence, (IDBEntityTypeDescriptor) entityTypeDescriptor2, byPropertyName.DBRelationAttributes);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation dbRelation = sessionKeeper.Session.GetRelationCollection(byPropertyName.DBRelationType.Id).Create(key1, key2);
      if (modifiedAttributes.SimpleAttributes.Length != 0)
        dbRelation.SetAttributesValues(modifiedAttributes.SimpleAttributes);
      if (modifiedAttributes.FileAttributes.Count != 0)
        this.UpdateFileAttributes(childOccurence, (IDBAttributable) dbRelation, modifiedAttributes.FileAttributes);
      if (modifiedAttributes.RemovedAttributes.Count != 0)
        this.RemoveAttributes((IDBAttributable) dbRelation, modifiedAttributes.RemovedAttributes);
      List<DataPropertyMapping> allAsList = CollectionUtils.FindAllAsList<DataPropertyMapping>(byPropertyName.DBRelationAttributes.AsCollection, (Predicate<DataPropertyMapping>) (item => item.IsObligatory));
      if (allAsList.Count == 0)
        return;
      AttributeValues[] attributesValues = dbRelation.GetAttributesValues(GetAttributeValuesModes.IncludeObligatoryAttributes);
      foreach (DataPropertyMapping dataPropertyMapping in allAsList)
      {
        DataPropertyMapping propertyMapping = dataPropertyMapping;
        AttributeValues dbAttributeData = CollectionUtils.Find<AttributeValues>((IEnumerable<AttributeValues>) attributesValues, (Predicate<AttributeValues>) (x => x.AttributeID == propertyMapping.Id));
        object propertyValue = this.ConvertToPropertyValue(propertyMapping, dbAttributeData);
        this.SetEntityPropertyValue((IDBEntityTypeDescriptor) entityTypeDescriptor2, childOccurence, propertyMapping, propertyValue);
      }
    }
  }

  public void UpdateDBRelationAttributes(
    object parentObjectEntity,
    IDBObjectEntityTypeDescriptor parentDescriptor,
    string propertyName,
    object relationEntity,
    IDBRelationEntityTypeDescriptor relationDescriptor,
    List<string> modifiedProperties)
  {
    if (modifiedProperties.Count == 0)
      return;
    DBObjectNavigationPropertyMapping byPropertyName = parentDescriptor.NavigationPropertiesMappings.GetByPropertyName(propertyName, true);
    InternalDataService.ModifiedAttributes modifiedAttributes = this.ConvertToModifiedAttributes(relationEntity, (IDBEntityTypeDescriptor) relationDescriptor, byPropertyName.DBRelationAttributes, (ICollection<string>) modifiedProperties);
    if (modifiedAttributes.IsEmpty())
      return;
    long key = relationDescriptor.GetKey(relationEntity);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(key);
      if (!this.IsAliveDBRelation(relation))
        throw new DBUpdateConcurrencyException("Невозможно записать атрибуты связи, так как она уже была удалена.");
      if (modifiedAttributes.SimpleAttributes.Length != 0)
        relation.SetAttributesValues(modifiedAttributes.SimpleAttributes);
      if (modifiedAttributes.FileAttributes.Count != 0)
        this.UpdateFileAttributes(relationEntity, (IDBAttributable) relation, modifiedAttributes.FileAttributes);
      if (modifiedAttributes.RemovedAttributes.Count == 0)
        return;
      this.RemoveAttributes((IDBAttributable) relation, modifiedAttributes.RemovedAttributes);
    }
  }

  public void RemoveSimpleDBRelation(object parentEntity, string propertyName, object childEntity)
  {
    if (parentEntity == null)
      throw new ArgumentNullException(nameof (parentEntity));
    if (propertyName == null)
      throw new ArgumentNullException(nameof (propertyName));
    if (childEntity == null)
      throw new ArgumentNullException(nameof (childEntity));
    IDBObjectEntityTypeDescriptor entityTypeDescriptor = this.Configuration.GetEntityTypeDescriptor(parentEntity).AsDBObjectDescriptor();
    DBObjectNavigationPropertyMapping byPropertyName = entityTypeDescriptor.NavigationPropertiesMappings.GetByPropertyName(propertyName, true);
    long key1 = entityTypeDescriptor.GetKey(parentEntity);
    long key2 = this.Configuration.GetEntityTypeDescriptor(childEntity).AsDBObjectDescriptor().GetKey(childEntity);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
      sessionKeeper.Session.GetRelation(key1, key2, byPropertyName.DBRelationType.Id, true)?.Delete(0L);
  }

  public void RemoveComplexDBRelation(
    object parentEntity,
    string propertyName,
    object childEntity,
    object relationEntity,
    IDBRelationEntityTypeDescriptor relationDescriptor)
  {
    long key = relationDescriptor.GetKey(relationEntity);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation = sessionKeeper.Session.GetRelation(key);
      if (!this.IsAliveDBRelation(relation))
        return;
      relation.Delete(0L);
    }
  }

  public void UpdateComplexDBRelationKey(
    object parentEntity,
    IDBObjectEntityTypeDescriptor parentDescriptor,
    string propertyName,
    object childEntity,
    object relationEntity,
    IDBRelationEntityTypeDescriptor relationDescriptor)
  {
    long key1 = parentDescriptor.GetKey(parentEntity);
    long key2 = relationDescriptor.GetKey(relationEntity);
    using (SessionKeeper sessionKeeper = new SessionKeeper())
    {
      IDBRelation relation1 = sessionKeeper.Session.GetRelation(key2);
      if (!this.IsAliveDBRelation(relation1) || relation1.ProjID == key1)
        return;
      IDBRelation relation2 = sessionKeeper.Session.GetRelation(relation1.GUID, key1, false);
      if (!this.IsAliveDBRelation(relation2))
        return;
      relationDescriptor.SetKey(relationEntity, relation2.RelationID);
    }
  }

  private bool IsAliveDBRelation(IDBRelation dbRelation) => dbRelation != null;

  private long GetEmptyEntityKey(object entity, IDBObjectEntityTypeDescriptor entityTypeDescriptor)
  {
    long key = entityTypeDescriptor.GetKey(entity);
    this.CheckEntityKeyIsEmpty(key, entityTypeDescriptor);
    return key;
  }

  private void CheckEntityKeyIsEmpty(
    long entityKey,
    IDBObjectEntityTypeDescriptor entityTypeDescriptor)
  {
    if (entityKey != 0L)
      throw new EntityValidationException($"Идентификатор версии доменного объекта '{entityTypeDescriptor.EntityType}' должен быть не задан и равен {0L}.");
  }

  private long GetDefinedEntityKey(
    object entity,
    IDBObjectEntityTypeDescriptor entityTypeDescriptor)
  {
    long key = entityTypeDescriptor.GetKey(entity);
    this.CheckEntityKeyIsDefined(key, entityTypeDescriptor);
    return key;
  }

  private void CheckEntityKeyIsDefined(
    long entityKey,
    IDBObjectEntityTypeDescriptor entityTypeDescriptor)
  {
    if (entityKey == 0L)
      throw new EntityValidationException($"Не задан идентификатор версии у доменного объекта '{entityTypeDescriptor.EntityType}'.");
  }

  private void SetEntityPropertyValue(
    IDBEntityTypeDescriptor entityTypeDescriptor,
    object entity,
    DataPropertyMapping propertyMapping,
    object propertyValue)
  {
    if (propertyValue != null && propertyValue is IPropertyValueWithLoader)
      ((IPropertyValueWithLoader) propertyValue).SetEntity(entityTypeDescriptor, entity);
    propertyMapping.PropertyDescriptor.SetValue(entity, propertyValue);
  }

  private object ConvertToPropertyValue(
    DataPropertyMapping dbAttributeMapping,
    AttributeValues dbAttributeData)
  {
    return dbAttributeData != null && dbAttributeData.Values != null && dbAttributeData.Values.Length != 0 && !Convert.IsDBNull(dbAttributeData.Values[0]) ? this.ConvertToPropertyValue(dbAttributeMapping, dbAttributeData.Values[0], 0) : dbAttributeMapping.ValueLoadParameters.DBNullEquivalent;
  }

  private object ConvertToPropertyValue(
    DataPropertyMapping dbAttributeMapping,
    DataRow tableRow,
    int columnIndex)
  {
    object dbValue = tableRow[columnIndex];
    return !Convert.IsDBNull(dbValue) ? this.ConvertToPropertyValue(dbAttributeMapping, dbValue, 0) : dbAttributeMapping.ValueLoadParameters.DBNullEquivalent;
  }

  private object ConvertToPropertyValue(
    DataPropertyMapping propertyMapping,
    object dbValue,
    int dbValueIndex)
  {
    Type meaningfulValueType = propertyMapping.ValueLoadParameters.MeaningfulValueType;
    if (meaningfulValueType == typeof (MeasuredValue) && this.IsDBStringValue(dbValue))
      return (object) MeasureHelper.ConvertToMeasuredValue(Convert.ToString(dbValue));
    if (meaningfulValueType == typeof (DBFileValue) && this.IsDBInteger(dbValue))
      return (object) new DBFileValue(new DBFileValueLoader(propertyMapping.Id, dbValueIndex));
    return meaningfulValueType == typeof (Guid) && this.IsDBStringValue(dbValue) ? (object) new Guid(Convert.ToString(dbValue)) : Convert.ChangeType(dbValue, meaningfulValueType);
  }

  private bool IsDBStringValue(object dbValue) => dbValue is string;

  private bool IsDBInteger(object dbValue) => dbValue is long || dbValue is Decimal;

  private InternalDataService.ModifiedAttributes ConvertToModifiedAttributes(
    object entity,
    IDBEntityTypeDescriptor entityTypeDescriptor,
    DataPropertyMappings dataPropertyMappings,
    ICollection<string> modifiedPropertiesFilter = null)
  {
    bool isCreateMode = modifiedPropertiesFilter == null;
    ICollection<string> strings = modifiedPropertiesFilter != null ? modifiedPropertiesFilter : dataPropertyMappings.PropertyNames;
    InternalDataService.ModifiedAttributes modifiedAttributes = new InternalDataService.ModifiedAttributes();
    foreach (string propertyName in (IEnumerable<string>) strings)
    {
      DataPropertyMapping byPropertyName = dataPropertyMappings.GetByPropertyName(propertyName, false);
      if (byPropertyName != null && !byPropertyName.IsObligatory)
      {
        EntityPropertyData propertyData = byPropertyName.PropertyDescriptor.GetValue(entity);
        if (propertyData.PresenceStatus != EntityMemberPresenceStatus.NotPresent)
          this.ConvertToModifiedAttributeValues(entity, byPropertyName, propertyData, isCreateMode, modifiedAttributes);
      }
    }
    return modifiedAttributes;
  }

  private void ConvertToModifiedAttributeValues(
    object entity,
    DataPropertyMapping dataPropertyMapping,
    EntityPropertyData propertyData,
    bool isCreateMode,
    InternalDataService.ModifiedAttributes modifiedAttributes)
  {
    if (dataPropertyMapping.IsFileOrBlob)
      modifiedAttributes.AddModifiedFileAttribute(new InternalDataService.ModifiedSpecialAttribute(dataPropertyMapping, propertyData, isCreateMode));
    else
      this.ConvertSimplePropertyToModifiedAttributeValues(entity, dataPropertyMapping, propertyData, isCreateMode, modifiedAttributes);
  }

  private void ConvertSimplePropertyToModifiedAttributeValues(
    object entity,
    DataPropertyMapping dataPropertyMapping,
    EntityPropertyData propertyData,
    bool isCreateMode,
    InternalDataService.ModifiedAttributes modifiedAttributes)
  {
    object propertyValue = propertyData.PropertyValue;
    DataPropertyLanguageInfo languageInfo = dataPropertyMapping.LanguageInfo;
    DataPropertySaveParameters valueSaveParameters = dataPropertyMapping.ValueSaveParameters;
    if (propertyValue == null && languageInfo.HasEmptyValue)
      throw new EntityValidationException(entity, $"У доменного объекта '{entity}' свойство '{dataPropertyMapping.PropertyDescriptor.Definition.Name}' не может быть равным null. Вместо null следует использовать пустое значение.");
    if ((propertyValue == null ? 1 : (!languageInfo.HasEmptyValue ? 0 : (object.Equals(propertyValue, languageInfo.EmptyValue) ? 1 : 0))) != 0)
    {
      if (valueSaveParameters.NullSaveMode == DBNullSaveMode.NotSupported)
        throw this.InvalidDataPropertyValue(entity, dataPropertyMapping, propertyData);
      if (isCreateMode && valueSaveParameters.IgnoreNullValueOnCreate)
        return;
      if (valueSaveParameters.RemoveNullValueOnUpdate)
      {
        modifiedAttributes.AddRemovedAttribute(dataPropertyMapping.Id);
        return;
      }
      propertyValue = (object) DBNull.Value;
    }
    modifiedAttributes.AddModifiedSimpleAttribute(new AttributeValues(dataPropertyMapping.Id, propertyValue)
    {
      IsNew = true,
      ThrowSetException = true
    });
  }

  private void RemoveAttributes(IDBAttributable dbAttributable, ICollection<int> removedAttributes)
  {
    foreach (int removedAttribute in (IEnumerable<int>) removedAttributes)
      dbAttributable.GetAttributeByID(removedAttribute)?.Delete(0L);
  }

  private void UpdateFileAttributes(
    object entity,
    IDBAttributable dbAttributable,
    ICollection<InternalDataService.ModifiedSpecialAttribute> fileAttributes)
  {
    foreach (InternalDataService.ModifiedSpecialAttribute fileAttribute in (IEnumerable<InternalDataService.ModifiedSpecialAttribute>) fileAttributes)
      this.UpdateFileAttribute(entity, dbAttributable, fileAttribute);
  }

  private void UpdateFileAttribute(
    object entity,
    IDBAttributable dbAttributable,
    InternalDataService.ModifiedSpecialAttribute attribute)
  {
    DataPropertyMapping attributeMapping = attribute.DBAttributeMapping;
    IDBAttribute dbAttribute = dbAttributable.GetAttributeByID(attributeMapping.Id);
    object propertyValue = attribute.PropertyData.PropertyValue;
    if (propertyValue != null && !object.Equals(propertyValue, attributeMapping.LanguageInfo.EmptyValue))
    {
      if (dbAttribute == null)
        dbAttribute = dbAttributable.Attributes.AddAttribute(attributeMapping.Id, true);
      this.WriteFileContent(dbAttributable, dbAttribute, attribute);
    }
    else
    {
      if (dbAttribute == null)
        return;
      if (attributeMapping.AllowDBNull)
      {
        this.ClearFileContent(dbAttributable, dbAttribute, attribute);
      }
      else
      {
        if (!attributeMapping.IsDeletable)
          throw this.InvalidDataPropertyValue(entity, attributeMapping, attribute.PropertyData);
        dbAttribute.Delete(0L);
      }
    }
  }

  private void WriteFileContent(
    IDBAttributable dbAttributable,
    IDBAttribute dbAttribute,
    InternalDataService.ModifiedSpecialAttribute attribute)
  {
    DBFileValue propertyValue = (DBFileValue) attribute.PropertyData.PropertyValue;
    DateTime modifyDate = DateTime.UtcNow.TruncateToSecond() + dbAttribute.Session.TimeZoneOffset;
    BlobInformation aBlobInformation = new BlobInformation((long) propertyValue.Content.Length, (long) propertyValue.Content.Length, modifyDate, propertyValue.Name, ArcMethods.NotPacked, string.Empty);
    aBlobInformation.FileType = FileTypes.ftNormal;
    using (MemoryStream aSourceStream = new MemoryStream(propertyValue.Content))
    {
      aSourceStream.Position = 0L;
      new BlobProcWriter(dbAttribute.DBObjectID, AttributableElements.Object, dbAttribute.AttributeID, dbAttribute.Index, 0, aBlobInformation, (Stream) aSourceStream, (BlobProcCustomClass.ProgressEventHandler) null, (BlobProcCustomClass.ThreadFinishEventHandler) null).WriteData();
    }
  }

  private void ClearFileContent(
    IDBAttributable dbAttributable,
    IDBAttribute dbAttribute,
    InternalDataService.ModifiedSpecialAttribute attribute)
  {
    dbAttribute.Clear();
  }

  private EntityValidationException InvalidDataPropertyValue(
    object entity,
    DataPropertyMapping propertyMapping,
    EntityPropertyData propertyData)
  {
    return new EntityValidationException(entity, $"У доменного объекта '{entity}' свойство '{propertyMapping.PropertyDescriptor.Definition.Name}' не может быть равным '{propertyData.PropertyValue}', так как это значение не может быть записано в базу данных.");
  }

  /// <summary>
  /// Контейнер для атрибутов объекта или связи IPS, которые требуется записать или удалить из базы данных.
  /// Объекты этого типа используются при планировании записи изменений.
  /// </summary>
  private sealed class ModifiedAttributes
  {
    private List<AttributeValues> simpleAttributesList;
    private AttributeValues[] simpleAttributesArrayCache;
    private List<InternalDataService.ModifiedSpecialAttribute> fileAttributesList;
    private ICollection<InternalDataService.ModifiedSpecialAttribute> fileAttributesROView;
    private List<int> removedAttributesList;
    private ICollection<int> removedAttributesROView;
    private static readonly AttributeValues[] emptySimpleAttributes = new AttributeValues[0];
    private static readonly ICollection<InternalDataService.ModifiedSpecialAttribute> emptySpecialAttributes = (ICollection<InternalDataService.ModifiedSpecialAttribute>) new ReadOnlyCollectionWrapper<InternalDataService.ModifiedSpecialAttribute>((ICollection<InternalDataService.ModifiedSpecialAttribute>) new InternalDataService.ModifiedSpecialAttribute[0]);
    private static readonly ICollection<int> emptyRemovedAttributes = (ICollection<int>) new ReadOnlyCollectionWrapper<int>((ICollection<int>) new int[0]);

    /// <summary>Создает объект.</summary>
    public ModifiedAttributes()
    {
      this.fileAttributesROView = InternalDataService.ModifiedAttributes.emptySpecialAttributes;
      this.removedAttributesROView = InternalDataService.ModifiedAttributes.emptyRemovedAttributes;
    }

    /// <summary>
    /// Добавляет в контейнер простой атрибут, записываемый стандартным способом через <see cref="M:Intermech.Interfaces.IDBAttributable.SetAttributesValues(Intermech.Interfaces.AttributeValues[])" />.
    /// </summary>
    /// <param name="attribute">Описатель значения атрибута</param>
    public void AddModifiedSimpleAttribute(AttributeValues attribute)
    {
      if (this.simpleAttributesList == null)
        this.simpleAttributesList = new List<AttributeValues>();
      this.simpleAttributesList.Add(attribute);
      if (this.simpleAttributesArrayCache == null)
        return;
      this.simpleAttributesArrayCache = (AttributeValues[]) null;
    }

    /// <summary>
    /// Добавляет в контейнер файловый атрибут, записываемый особым способом.
    /// </summary>
    /// <param name="attribute">Описатель значения атрибута</param>
    public void AddModifiedFileAttribute(
      InternalDataService.ModifiedSpecialAttribute attribute)
    {
      if (this.fileAttributesList == null)
      {
        this.fileAttributesList = new List<InternalDataService.ModifiedSpecialAttribute>();
        this.fileAttributesROView = (ICollection<InternalDataService.ModifiedSpecialAttribute>) new ReadOnlyCollectionWrapper<InternalDataService.ModifiedSpecialAttribute>((ICollection<InternalDataService.ModifiedSpecialAttribute>) this.fileAttributesList);
      }
      this.fileAttributesList.Add(attribute);
    }

    /// <summary>
    /// Добавляет в контейнер атрибут, который требуется удалить из базы данных.
    /// </summary>
    /// <param name="attributeId">Идентификатор атрибута</param>
    public void AddRemovedAttribute(int attributeId)
    {
      if (this.removedAttributesList == null)
      {
        this.removedAttributesList = new List<int>();
        this.removedAttributesROView = (ICollection<int>) new ReadOnlyCollectionWrapper<int>((ICollection<int>) this.removedAttributesList);
      }
      this.removedAttributesList.Add(attributeId);
    }

    /// <summary>Возвращает признак, что контейнер пуст.</summary>
    /// <returns>Признак пустого контейнера</returns>
    public bool IsEmpty()
    {
      return this.SimpleAttributes.Length == 0 && this.FileAttributes.Count == 0 && this.RemovedAttributes.Count == 0;
    }

    /// <summary>
    /// Возвращает read-only массив простых атрибутов, которые требуется записать в базу данных.
    /// Если таких атрибутов нет, то массив будет пуст.
    /// </summary>
    public AttributeValues[] SimpleAttributes
    {
      [DebuggerStepThrough] get
      {
        if (this.simpleAttributesArrayCache == null)
          this.simpleAttributesArrayCache = this.simpleAttributesList != null ? this.simpleAttributesList.ToArray() : InternalDataService.ModifiedAttributes.emptySimpleAttributes;
        return this.simpleAttributesArrayCache;
      }
    }

    /// <summary>
    /// Возвращает read-only коллекцию файловых атрибутов, которые требуется записать в базу данных.
    /// Если таких атрибутов нет, то коллекция будет пуста.
    /// </summary>
    public ICollection<InternalDataService.ModifiedSpecialAttribute> FileAttributes
    {
      [DebuggerStepThrough] get => this.fileAttributesROView;
    }

    /// <summary>
    /// Возвращает read-only коллекцию идентификаторов атрибутов, которые требуется удалить из базы данных.
    /// Если таких атрибутов нет, то коллекция будет пуста.
    /// </summary>
    public ICollection<int> RemovedAttributes
    {
      [DebuggerStepThrough] get => this.removedAttributesROView;
    }
  }

  /// <summary>
  /// Описатель нового или измененного атрибута объекта или связи IPS, который требует особого способа записи в базу данных.
  /// Остальные атрибуты записываются стандартным способом через <see cref="M:Intermech.Interfaces.IDBAttributable.SetAttributesValues(Intermech.Interfaces.AttributeValues[])" />.
  /// </summary>
  private sealed class ModifiedSpecialAttribute
  {
    public ModifiedSpecialAttribute(
      DataPropertyMapping dbAttributeMapping,
      EntityPropertyData propertyData,
      bool isCreateMode)
    {
      this.DBAttributeMapping = dbAttributeMapping;
      this.PropertyData = propertyData;
      this.IsCreateMode = isCreateMode;
    }

    /// <summary>
    /// Возвращает или задает описатель отображения свойства доменного объекта в атрибут IPS.
    /// </summary>
    public DataPropertyMapping DBAttributeMapping { get; private set; }

    /// <summary>
    /// Возвращает или задает контейнер с значением свойства доменного объекта.
    /// </summary>
    public EntityPropertyData PropertyData { get; private set; }

    /// <summary>
    /// Признак режима создания доменного объекта в базе данных IPS.
    /// </summary>
    public bool IsCreateMode { get; private set; }
  }
}
