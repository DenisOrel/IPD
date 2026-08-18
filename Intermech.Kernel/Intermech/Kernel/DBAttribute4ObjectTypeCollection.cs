// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAttribute4ObjectTypeCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;


namespace Intermech.Kernel;

internal class DBAttribute4ObjectTypeCollection : 
  BasicAttributeTypeCollection,
  IDBAttribute4ObjectTypeCollection,
  IDBAttribute4TypeCollection,
  IDBCollection
{
  private FieldTypes _ftFilter;
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(3);
  internal bool AutoPatchMode;

  public DBAttribute4ObjectTypeCollection(UserSession uSession, int objectTypeID, bool filterRecs)
    : base(uSession, filterRecs)
  {
    this.ParentID = (object) objectTypeID;
    this._DBTypeField = "F_OBJECT_TYPE";
    this._DBTableName = "IMS_ATTR4OBJ_TYPES";
    this._DBKeyField = "F_ATTRIBUTE_ID";
    this._AreaSupport = filterRecs;
    this._LanguageSupport = filterRecs;
    this.InitSecurityOptions(4, (long) objectTypeID);
    this._SelectFromCache = true;
  }

  static DBAttribute4ObjectTypeCollection()
  {
    DBAttribute4ObjectTypeCollection.metadataActions.Add(ActionType.GetAccess, false);
    DBAttribute4ObjectTypeCollection.metadataActions.Add(ActionType.SetAccess, false);
    DBAttribute4ObjectTypeCollection.metadataActions.Add(ActionType.EditProperties, false);
  }

  public override bool AnyAttributes
  {
    get => this.UserSession.GetObjectType((int) this.ParentID).AnyAttributes;
  }

  public override AttributeSourceTypes CollectionSourceType => AttributeSourceTypes.Object;

  protected override IDBAttributeType GetAttributeType4(int attrID, bool failIfNotFound)
  {
    return (IDBAttributeType) this.GetAttributeByID(attrID, false) ?? this.UserSession.GetAttributeType(attrID, failIfNotFound);
  }

  public Attribute4ObjectTypeProperties GetDefaultProperties(int attributeID)
  {
    return AttributeCacheHelper.GetDefaultProperties(this.UserSession.GetAttributeTypeCollection(-1), attributeID, (int) this.ParentID);
  }

  public override DataTable Select(string orderBy, params object[] addInfo)
  {
    bool flag = false;
    if (addInfo != null)
    {
      foreach (object obj1 in addInfo)
      {
        object obj2;
        switch (obj1)
        {
          case string _ when obj2.ToString() == "ALL_FIELDS":
            flag = true;
            break;
          case FieldTypes fieldTypes:
            this._ftFilter = fieldTypes;
            break;
        }
      }
    }
    DataTable destinationTable = base.Select(orderBy, addInfo);
    if (this._ftFilter != FieldTypes.ftUnknown && this._DBTableName == "IMS_ATTR4OBJ_TYPES")
    {
      for (int index = destinationTable.Rows.Count - 1; index >= 0; --index)
      {
        if (this._ftFilter != this.UserSession.GetAttributeType(Convert.ToInt32(destinationTable.Rows[index]["F_ATTRIBUTE_ID"])).AttributeType)
          destinationTable.Rows.RemoveAt(index);
      }
      destinationTable.AcceptChanges();
      this._ftFilter = FieldTypes.ftUnknown;
    }
    if (flag)
      AttributeCacheHelper.AddFieldsForAttribute(this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES"), destinationTable);
    return destinationTable;
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBAttribute4ObjectTypeCollection.metadataActions);
  }

  public override string ObjectName
  {
    get
    {
      IDBObjectType objectType = this.UserSession.GetObjectType((int) this.ParentID);
      return string.Format(LocalizationHolder.rm.GetString("Kernel_144"), (object) objectType.ObjectTypeName);
    }
  }

  public override object ParentID
  {
    get => base.ParentID;
    set
    {
      if (this.ParentID == value)
        return;
      if (this.UserSession.DBCache.GetTable("IMS_OBJECT_TYPES").Rows.Find(value) == null)
        throw new KernelException(LocalizationHolder.rm.GetString(sc_12431.ssp_appserver_12445()) + value.ToString());
      base.ParentID = value;
    }
  }

  protected override string GetParentSQL(object parentID)
  {
    return this._ftFilter != FieldTypes.ftUnknown ? $"(F_OBJECT_TYPE = {parentID} AND F_ATTRIBUTE_TYPE = {(int) this._ftFilter})" : $"(F_OBJECT_TYPE = {parentID.ToString()})";
  }

  public override IDBAttributeType4 GetAttributeByID(int attributeID, bool throwNotFoundException)
  {
    IDBAttributeType4 attributeById1;
    if (this.Attributes.TryGetValue(attributeID, out attributeById1))
      return attributeById1;
    DataRow row = this.UserSession.DBCache.GetTable("IMS_ATTR4OBJ_TYPES").Rows.Find(new object[2]
    {
      (object) attributeID,
      (object) (int) this.ParentID
    });
    if (row != null)
    {
      IDBAttributeType4 attributeById2;
      switch (Convert.ToInt32(this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES").Rows.Find((object) attributeID)["F_ATTRIBUTE_TYPE"]))
      {
        case 8:
          attributeById2 = (IDBAttributeType4) new DBObjectLinkAttributeType4Object(this.UserSession, row);
          break;
        case 13:
          attributeById2 = (IDBAttributeType4) new DBMeasureAttributeType4Object(this.UserSession, row);
          break;
        case 17:
          attributeById2 = (IDBAttributeType4) new DBObjectLinkByIDAttributeType4Object(this.UserSession, row);
          break;
        default:
          attributeById2 = (IDBAttributeType4) new DBAttributeType4Object(this.UserSession, row);
          break;
      }
      this.Attributes.Add(attributeID, attributeById2);
      return attributeById2;
    }
    if (throwNotFoundException)
      throw new AttributeTypeNotFoundException();
    return (IDBAttributeType4) null;
  }

  public IDBAttributeType4 GetAttributeByID(int attributeID)
  {
    return this.GetAttributeByID(attributeID, false);
  }

  public IDBAttributeType4 GetAttributeByName(string attributeName, bool throwNotFoundException)
  {
    int attributeByTypeNameId = MetaDataHelper.GetAttributeByTypeNameID(attributeName);
    if (attributeByTypeNameId != -10000)
      return this.GetAttributeByID(attributeByTypeNameId, throwNotFoundException);
    if (throwNotFoundException)
      throw new AttributeTypeNotFoundException(string.Format(LocalizationHolder.rm.GetString(sc_12431.ssp_appserver_12446()), (object) attributeName));
    return (IDBAttributeType4) null;
  }

  public IDBAttributeType4 GetAttributeByName(string attributeName)
  {
    return this.GetAttributeByName(attributeName, false);
  }

  public IDBAttributeType4 GetAttributeByGUID(Guid attributeGuid, bool throwNotFoundException)
  {
    int attributeTypeId = MetaDataHelper.GetAttributeTypeID(attributeGuid);
    if (attributeTypeId != -10000)
      return this.GetAttributeByID(attributeTypeId, throwNotFoundException);
    if (throwNotFoundException)
      throw new AttributeTypeNotFoundException(string.Format(LocalizationHolder.rm.GetString(sc_12431.ssp_appserver_12447()), (object) attributeGuid));
    return (IDBAttributeType4) null;
  }

  public IDBAttributeType4 GetAttributeByGUID(Guid attributeGuid)
  {
    return this.GetAttributeByGUID(attributeGuid, false);
  }

  private void CheckInheritedAttributes(
    Attribute4ObjectTypeProperties attrProperties,
    int objTypeID)
  {
    this.UserSession.DBCache.EnterReadLocker();
    try
    {
      foreach (DataRow dataRow in this.UserSession.DBCache.GetTable("IMS_OBJTYPES_TREE").Select("F_PARENT_ID = " + objTypeID.ToString()))
      {
        int int32 = Convert.ToInt32(dataRow["F_OBJECT_TYPE"]);
        if (Convert.ToInt32(this.UserSession.DBCache.GetTable(this._DBTableName).Rows.Find(new object[2]
        {
          (object) attrProperties.AttributeID,
          (object) int32
        })["F_PUBLIC"]) == 2)
        {
          if (attrProperties.InheritMode == InheritModes.Private)
            throw new KernelExceptionID(sc_12431.ssp_appserver_12448(30448257), (object) this.UserSession.GetAttributeType(attrProperties.AttributeID).Name);
          Attribute4ObjectTypeProperties propertiesStructure = (this.UserSession.GetObjectType(int32).Attributes.GetAttributeByID(attrProperties.AttributeID) as IDBAttributeType4Object).Attribute4ObjectPropertiesStructure with
          {
            InheritMode = attrProperties.InheritMode,
            ObjectType = attrProperties.ObjectType,
            FieldType = attrProperties.FieldType
          };
          if (!attrProperties.Equals((object) propertiesStructure))
            throw new KernelExceptionID(sc_12431.ssp_appserver_12449(1805140062), (object) this.UserSession.GetAttributeType(attrProperties.AttributeID).Name);
          this.CheckInheritedAttributes(attrProperties, int32);
        }
      }
    }
    finally
    {
      this.UserSession.DBCache.ExitReadLocker();
    }
  }

  private int GetParentType4Attribute(int childType, int attributeID)
  {
    DataRow[] dataRowArray = this.UserSession.DBCache.GetTable("IMS_OBJTYPES_TREE").Select("F_OBJECT_TYPE = " + childType.ToString());
    if (dataRowArray != null && dataRowArray.Length != 0)
    {
      int int32 = Convert.ToInt32(dataRowArray[0]["F_PARENT_ID"]);
      IDBAttributeType4Object attributeById = this.UserSession.GetObjectType(int32).Attributes.GetAttributeByID(attributeID, false) as IDBAttributeType4Object;
      if (attributeById.InheritMode == InheritModes.Inherited)
        return this.GetParentType4Attribute(int32, attributeID);
      if (attributeById.InheritMode == InheritModes.Public)
        return int32;
    }
    return childType;
  }

  private void CheckChangeEnable(int attributeID)
  {
    int parentType4Attribute = this.GetParentType4Attribute(Convert.ToInt32(this.ParentID), attributeID);
    if (parentType4Attribute != Convert.ToInt32(this.ParentID) && !this.UserSession.CanChangeObjectElement(4, (object) parentType4Attribute, ObligatoryElementKeys.GetKeyForAttributeProperty(attributeID, "F_PUBLIC")))
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_943"), (object) DataSetProcessor.GetCaption("F_PUBLIC"), (object) this.UserSession.GetAttributeType(attributeID).Name, (object) this.UserSession.GetObjectType(Convert.ToInt32(this.ParentID)).ObjectTypeName));
  }

  public IDBAttributeType4Object Create(Attribute4ObjectTypeProperties attrProperties)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    IDBAttributeType attributeType = this.UserSession.GetAttributeType(attrProperties.AttributeID);
    IDBObjectType objectType = this.UserSession.GetObjectType((int) this.ParentID);
    this._LastEventID = (objectType as DBObjectType).AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString("Kernel_146"), (object) attributeType.Name));
    this.UserSession.StartTransaction();
    try
    {
      this.CheckAccess(ActionType.EditProperties);
      if (attributeType is DBSystemAttributeType)
        throw new KernelExceptionID(sc_12431.ssp_appserver_12450(935771342));
      if (attrProperties.InheritMode == InheritModes.Inherited)
        throw new KernelException("Invalid InheritMode value: " + InheritModes.Inherited.ToString());
      DataRow dataRow = this.UserSession.DBCache.GetTable(this._DBTableName).Rows.Find(new object[2]
      {
        (object) attrProperties.AttributeID,
        this.ParentID
      });
      OptimizationModes optimizationModes = OptimizationModes.Write;
      if (dataRow != null)
      {
        if (Convert.ToInt32(dataRow["F_PUBLIC"]) != 2)
          throw new KernelException(string.Format(LocalizationHolder.rm.GetString("Kernel_147"), (object) attributeType.Name, (object) objectType.ObjectTypeName));
        this.CheckInheritedAttributes(attrProperties, Convert.ToInt32(this.ParentID));
        this.CheckChangeEnable(attrProperties.AttributeID);
        string condition = $"F_OBJECT_TYPE = {this.ParentID} AND F_ATTRIBUTE_ID = {attrProperties.AttributeID}";
        dataManager.ExecuteNonQuery(sc_12431.ssp_appserver_12451() + condition);
        this.UserSession.DBCache.DeleteRecords("IMS_ATTR4OBJ_TYPES", condition, (IUserSession) this.UserSession);
        optimizationModes = attrProperties.OptimizationMode;
      }
      dataManager.ExecuteNonQuery(string.Format(sc_12431.ssp_appserver_12452(), (object) attrProperties.AttributeID, this.ParentID, (object) Convert.ToInt32((object) InheritModes.Private), (object) Convert.ToInt32((object) RequiredModes.Manual), (object) Convert.ToInt32((object) optimizationModes)));
      DataTable dataTable = dataManager.ExecuteDataTable(string.Format(sc_12431.ssp_appserver_12453(), (object) attrProperties.AttributeID, this.ParentID));
      if (dataTable.Rows.Count <= 0)
        throw new KernelExceptionID(3);
      this.UserSession.DBCache.AddRow(this._DBTableName, dataTable.Rows[0], (IUserSession) this.UserSession);
      IDBAttributeType4Object attributeById = this.GetAttributeByID(attrProperties.AttributeID) as IDBAttributeType4Object;
      (attributeById as DBSessionable).LoggingOn = false;
      (attributeById as DBAttributeType4Object).AutoPatchMode = this.AutoPatchMode;
      attributeById.ValidationRule = attrProperties.ValidationRule;
      attributeById.Computed = attrProperties.ComputeValueMode;
      attributeById.Formula = attrProperties.Formula;
      attributeById.UniqueMode = attrProperties.UniqueValueMode;
      attributeById.LevelID = attrProperties.LevelID;
      if (attrProperties.DefaultValue == null)
        attributeById.DefaultValue = (object) string.Empty;
      else
        attributeById.DefaultValue = (object) Convert.ToString(attrProperties.DefaultValue, (IFormatProvider) CultureInfo.InvariantCulture);
      attributeById.Required = attrProperties.RequiredMode;
      attributeById.OptimizationMode = attrProperties.OptimizationMode;
      attributeById.InheritMode = attrProperties.InheritMode;
      attributeById.IsContent = attrProperties.IsContent;
      attributeById.Options = attrProperties.Options;
      attributeById.Mask = attrProperties.Mask;
      attributeById.MasterAttributeID = attrProperties.MasterAttributeID;
      attributeById.SourceAttributeID = attrProperties.SourceAttributeID;
      (attributeById as DBSessionable).LoggingOn = true;
      this.UserSession.Commit();
      return attributeById;
    }
    catch (Exception ex)
    {
      string str = string.Format(LocalizationHolder.rm.GetString(sc_12431.ssp_appserver_12454()), (object) attributeType.Name, (object) ex.Message);
      this.UserSession.Rollback();
      this.CloseEvent(this._LastEventID, EventlogRecordType.Error, str);
      this.UserSession.DBCache.ReloadTables((IUserSession) this.UserSession, dataManager, "IMS_ATTR4OBJ_TYPES");
      throw new KernelException(str, ex);
    }
  }
}
