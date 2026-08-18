// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.PortalServices.PublishTypesConfiguration
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.WebPortal;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Services.PortalServices;

public class PublishTypesConfiguration : LongLifeObject, IPublishTypesConfiguration
{
  private Dictionary<Guid, RelationMigrateType> _cacheRelationTypes;
  private readonly int _attributePublicationNecessaryID;
  private readonly int _attributePublisOptions;
  private readonly int _attributeObjectWithLinkID;
  private UserSession _session;
  private Dictionary<int, bool> _objectWithLinksCache;
  private readonly Guid _containerComplianceObjectGuid = new Guid("cadd94df-306c-11d8-b4e9-00304f19f545");

  public PublishTypesConfiguration(UserSession session)
  {
    this._session = session;
    this._attributePublicationNecessaryID = this._session.GetAttributeType(PortalConsts.attributePublicationNecessary).AttributeID;
    this._attributePublisOptions = this._session.GetAttributeType(PortalConsts.attributePublishOptions).AttributeID;
    this._attributeObjectWithLinkID = this._session.GetAttributeType(new Guid("cadd9bd9-306c-11d8-b4e9-00304f19f545")).AttributeID;
    (session.DBCache as CacheDataset).TableValueChanged += new TableChangedHandler(this.PublishTypesConfiguration_TableValueChanged);
    this._objectWithLinksCache = new Dictionary<int, bool>();
  }

  public List<int> PublishObjectTypes { get; private set; }

  private void PublishTypesConfiguration_TableValueChanged(
    object sender,
    TableChangedEventArgs args)
  {
    if (args == null || args.EventName != TableChangedEventNames.Delete)
      return;
    TableValueDeletedArgs valueDeletedArgs = args as TableValueDeletedArgs;
    if (valueDeletedArgs.DeletedRows == null || valueDeletedArgs.DeletedRows.Rows.Count == 0)
      return;
    if (valueDeletedArgs.TableName == "IMS_OBJECT_TYPES")
    {
      for (int index = 0; index < valueDeletedArgs.DeletedRows.Rows.Count; ++index)
      {
        int int32 = Convert.ToInt32(valueDeletedArgs.DeletedRows.Rows[index]["F_OBJECT_TYPE"]);
        if (this.PublishObjectTypes.Contains(int32))
          this.PublishObjectTypes.Remove(int32);
      }
      this.SaveObjectTypes();
    }
    else
    {
      if (!(valueDeletedArgs.TableName == "IMS_RELATION_TYPES"))
        return;
      for (int index = 0; index < valueDeletedArgs.DeletedRows.Rows.Count; ++index)
      {
        Guid key = new Guid(Convert.ToString(valueDeletedArgs.DeletedRows.Rows[index]["F_GUID"]));
        if (this._cacheRelationTypes.ContainsKey(key))
          this._cacheRelationTypes.Remove(key);
      }
      this.SaveRelationTypes();
    }
  }

  private void ReloadCompliances()
  {
    this.ComplianceObjectTypes = (Dictionary<string, Guid>) null;
    IDBObject dbObject = this._session.GetObject(this._containerComplianceObjectGuid, false);
    if (dbObject == null)
      return;
    IDBAttribute attributeByGuid = dbObject.GetAttributeByGuid(new Guid("cadd94dd-306c-11d8-b4e9-00304f19f545"), false);
    if (attributeByGuid == null || attributeByGuid.IsNull)
      return;
    this.ComplianceObjectTypes = new Dictionary<string, Guid>(attributeByGuid.ValuesCount);
    for (int index = 0; index < attributeByGuid.ValuesCount; ++index)
    {
      attributeByGuid.Index = index;
      string asString = attributeByGuid.AsString;
      if (!(asString == string.Empty))
      {
        string[] strArray = asString.Split('=');
        if (strArray.Length == 2 && strArray[0] != string.Empty && GuidHelper.IsGuid(strArray[1]) && !this.ComplianceObjectTypes.ContainsKey(strArray[0]))
          this.ComplianceObjectTypes.Add(strArray[0], new Guid(strArray[1]));
      }
    }
  }

  private void ReloadInternal()
  {
    this.PublishObjectTypes = new PublishObjectTypesCache().LoadCache((IUserSession) this._session);
    this._cacheRelationTypes = new PublishRelationTypesCache().LoadCache((IUserSession) this._session);
  }

  private void SaveRelationTypes()
  {
    lock (this._cacheRelationTypes)
      new PublishRelationTypesCache().SaveCache((IUserSession) this._session, this._cacheRelationTypes);
  }

  private void SaveObjectTypes()
  {
    lock (this.PublishObjectTypes)
      new PublishObjectTypesCache().SaveCache((IUserSession) this._session, this.PublishObjectTypes);
  }

  public void Save()
  {
    this._session.StartTransaction();
    try
    {
      this.SaveObjectTypes();
      this.SaveRelationTypes();
      this._session.Commit();
    }
    catch
    {
      this._session.Rollback();
      throw;
    }
  }

  public bool IsPublishObjectType(int objType)
  {
    return this.PublishObjectTypes != null && this.PublishObjectTypes.Contains(objType);
  }

  public void SetRelationMigrateType(
    Guid relationType,
    RelationMigrateType migrateType,
    bool saveInBase)
  {
    this._cacheRelationTypes[relationType] = migrateType;
    if (!saveInBase)
      return;
    this.SaveRelationTypes();
  }

  public void AddPublishObjectType(int objType, bool saveInBase)
  {
    if (this.PublishObjectTypes == null || this.PublishObjectTypes.Contains(objType))
      return;
    lock (this.PublishObjectTypes)
    {
      this._session.StartTransaction();
      try
      {
        this.AddPublishAttributes(objType);
        this.PublishObjectTypes.Add(objType);
        foreach (int num in MetaDataHelper.GetObjectTypeChildrenIDRecursive(objType))
        {
          if (!this.PublishObjectTypes.Contains(num))
            this.PublishObjectTypes.Add(num);
        }
        if (saveInBase)
          this.SaveObjectTypes();
        this._session.Commit();
      }
      catch (Exception ex)
      {
        this._session.Rollback();
        this.ReloadInternal();
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1091"), (object) MetaDataHelper.GetObjectTypeName(objType), (object) ex.Message));
      }
    }
  }

  private void DeletePublishAttributesFromChild(int objTypeID)
  {
    this._session.GetObjectType(objTypeID);
    List<int> objectTypeChildrenId = MetaDataHelper.GetObjectTypeChildrenID(objTypeID);
    if (objectTypeChildrenId == null || objectTypeChildrenId.Count == 0)
      return;
    this._session.StartTransaction();
    try
    {
      for (int index = 0; index < objectTypeChildrenId.Count; ++index)
      {
        if (objectTypeChildrenId[index] != objTypeID)
        {
          this.DeletePublishAttributes(objectTypeChildrenId[index]);
          this.DeletePublishAttributesFromChild(objectTypeChildrenId[index]);
        }
      }
      this._session.Commit();
    }
    catch
    {
      this._session.Rollback();
      throw;
    }
  }

  private void DeletePublishAttributes(int objTypeID)
  {
    IDBObjectType objectType = this._session.GetObjectType(objTypeID);
    this._session.StartTransaction();
    try
    {
      MetaDataHelper.Locked = true;
      IDBAttribute4ObjectTypeCollection attributes = (IDBAttribute4ObjectTypeCollection) objectType.Attributes;
      this.DeleteAttributeFromObjectType(attributes, this._attributePublisOptions);
      this.DeleteAttributeFromObjectType(attributes, this._attributePublicationNecessaryID);
      this._session.Commit();
    }
    catch
    {
      this._session.Rollback();
      throw;
    }
    finally
    {
      MetaDataHelper.Locked = false;
      MetaDataHelperUpdateService.AddTask(MetaDataHelperServiceUpdateTask.Full);
    }
  }

  private void DeleteAttributeFromObjectType(
    IDBAttribute4ObjectTypeCollection attrCollection,
    int attributeID)
  {
    IDBAttributeType4Object attributeById = (IDBAttributeType4Object) attrCollection.GetAttributeByID(attributeID);
    if (attributeById == null || attributeById.InheritMode == InheritModes.Inherited)
      return;
    attributeById.Delete(2L);
  }

  private void AddPublishAttributes(int objTypeID)
  {
    IDBObjectType objectType = this._session.GetObjectType(objTypeID);
    this._session.StartTransaction();
    try
    {
      MetaDataHelper.Locked = true;
      IDBAttribute4ObjectTypeCollection attributes = (IDBAttribute4ObjectTypeCollection) objectType.Attributes;
      this.AddAttributeTypeToObjectType(attributes, this._attributePublisOptions, objTypeID, (object) null);
      this.AddAttributeTypeToObjectType(attributes, this._attributePublicationNecessaryID, objTypeID, (object) 1);
      this._session.Commit();
    }
    catch
    {
      this._session.Rollback();
      throw;
    }
    finally
    {
      MetaDataHelper.Locked = false;
      MetaDataHelperUpdateService.AddTask(MetaDataHelperServiceUpdateTask.Full);
    }
  }

  private void AddAttributeTypeToObjectType(
    IDBAttribute4ObjectTypeCollection attrCollection,
    int attributeID,
    int objectTypeID,
    object defaultValue)
  {
    if (attrCollection.GetAttributeByID(attributeID) != null)
      return;
    attrCollection.Create(new Attribute4ObjectTypeProperties(attributeID, objectTypeID, InheritModes.Public, RequiredModes.Manual, string.Empty, ComputeValueModes.NotComputableValue, string.Empty, UniqueValueModes.NotUnique, 0, defaultValue, OptimizationModes.Seek, false, AttributeOptions.Internal | AttributeOptions.ModifyInBase, string.Empty, 0, 0));
  }

  public void RemovePublishObjectType(int objType, bool saveInBase)
  {
    lock (this.PublishObjectTypes)
    {
      this._session.StartTransaction();
      try
      {
        this.DeletePublishAttributes(objType);
        this.DeletePublishAttributesFromChild(objType);
        if (this.PublishObjectTypes.Contains(objType))
          this.PublishObjectTypes.Remove(objType);
        foreach (int num in MetaDataHelper.GetObjectTypeChildrenIDRecursive(objType))
        {
          if (this.PublishObjectTypes.Contains(num))
            this.PublishObjectTypes.Remove(num);
        }
        if (saveInBase)
          this.SaveObjectTypes();
        this._session.Commit();
      }
      catch (Exception ex)
      {
        this._session.Rollback();
        this.ReloadInternal();
        throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_1093"), (object) MetaDataHelper.GetObjectTypeName(objType), (object) ex.Message));
      }
    }
  }

  public void Reload()
  {
    this.ReloadInternal();
    this.ReloadCompliances();
    this.ReloadObjectWithLinksCache();
  }

  private void ReloadObjectWithLinksCache()
  {
    DataTable dataTable = this._session.GetObjectCollection(new Guid("cad0013b-306c-11d8-b4e9-00304f19f545")).Select(new DBRecordSetParams(new ConditionStructure[1]
    {
      new ConditionStructure(this._attributeObjectWithLinkID, RelationalOperators.AttributeExists, (object) null, LogicalOperators.AND, 0, false)
    }, new object[2]
    {
      (object) MetaDataHelper.GetAttributeTypeID("cad001a0-306c-11d8-b4e9-00304f19f545"),
      (object) this._attributeObjectWithLinkID
    }));
    this._objectWithLinksCache.Clear();
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
    {
      if (CompareValuesHelper.NormalizedValue(row[0]) != null)
      {
        string str = Convert.ToString(row[0]);
        if (GuidHelper.IsGuid(str))
        {
          int objectTypeId = MetaDataHelper.GetObjectTypeID(str);
          if (objectTypeId != -1 && !this._objectWithLinksCache.ContainsKey(objectTypeId))
            this._objectWithLinksCache.Add(objectTypeId, Convert.ToBoolean(row[1]));
        }
      }
    }
  }

  public Dictionary<string, Guid> ComplianceObjectTypes { get; private set; }

  public RelationMigrateType GetRelationMigrateType(Guid relationType)
  {
    RelationMigrateType relationMigrateType;
    return !this._cacheRelationTypes.TryGetValue(relationType, out relationMigrateType) ? RelationMigrateType.DependsSetting : relationMigrateType;
  }

  public CompositionApplicabilities GetCompositionApplicabilities()
  {
    List<string> stringList1 = new List<string>();
    List<string> stringList2 = new List<string>();
    foreach (KeyValuePair<Guid, RelationMigrateType> cacheRelationType in this._cacheRelationTypes)
    {
      if (cacheRelationType.Value == RelationMigrateType.Always)
        stringList2.Add(cacheRelationType.Key.ToString());
      else if (cacheRelationType.Value == RelationMigrateType.DependsSetting)
        stringList1.Add(cacheRelationType.Key.ToString());
    }
    return new CompositionApplicabilities(stringList1.Count > 0 ? stringList1.ToArray() : (string[]) null, stringList2.Count > 0 ? stringList2.ToArray() : (string[]) null);
  }

  public bool ObjectWithLink(int objType)
  {
    bool flag;
    return this._objectWithLinksCache.TryGetValue(objType, out flag) && flag;
  }

  public void SetObjectWithLink(int objType, bool value)
  {
    if (!(this._session.GetCustomService(typeof (IContainerService)) is IContainerService customService))
      return;
    IDBObject containerForObjectType = customService.GetContainerForObjectType((object) this._session.SessionGUID, objType, true);
    (containerForObjectType.GetAttributeByID(this._attributeObjectWithLinkID) ?? containerForObjectType.Attributes.AddAttribute(this._attributeObjectWithLinkID, false)).AsBoolean = value;
    if (this._objectWithLinksCache.ContainsKey(objType))
      this._objectWithLinksCache[objType] = value;
    else
      this._objectWithLinksCache.Add(objType, value);
  }

  public List<int> PublishRelationTypes
  {
    get
    {
      List<int> publishRelationTypes = new List<int>();
      foreach (KeyValuePair<Guid, RelationMigrateType> cacheRelationType in this._cacheRelationTypes)
      {
        if (cacheRelationType.Value != RelationMigrateType.None)
          publishRelationTypes.Add(MetaDataHelper.GetRelationTypeID(cacheRelationType.Key));
      }
      return publishRelationTypes;
    }
  }

  public List<int> AlwaysRelationTypes
  {
    get
    {
      List<int> intList = new List<int>();
      foreach (KeyValuePair<Guid, RelationMigrateType> cacheRelationType in this._cacheRelationTypes)
      {
        if (cacheRelationType.Value == RelationMigrateType.Always)
          intList.Add(MetaDataHelper.GetRelationTypeID(cacheRelationType.Key));
      }
      return intList.Count <= 0 ? (List<int>) null : intList;
    }
  }

  public bool ObjectWithLinksPresent => this._objectWithLinksCache.Count > 0;
}
