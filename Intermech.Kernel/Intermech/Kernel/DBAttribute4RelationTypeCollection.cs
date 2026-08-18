// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAttribute4RelationTypeCollection
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

internal class DBAttribute4RelationTypeCollection : 
  BasicAttributeTypeCollection,
  IDBAttribute4RelationTypeCollection,
  IDBAttribute4TypeCollection,
  IDBCollection
{
  private FieldTypes _ftFilter;
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(3);

  public DBAttribute4RelationTypeCollection(
    UserSession uSession,
    int relationTypeID,
    bool filterRecs)
    : base(uSession, filterRecs)
  {
    this.ParentID = (object) relationTypeID;
    this._DBTypeField = "F_RELATION_TYPE";
    this._DBTableName = "IMS_ATTR4RELATION_TYPES";
    this._DBKeyField = "F_ATTRIBUTE_ID";
    this._AreaSupport = filterRecs;
    this._LanguageSupport = filterRecs;
    this.InitSecurityOptions(6, (long) relationTypeID);
    this._SelectFromCache = true;
  }

  static DBAttribute4RelationTypeCollection()
  {
    DBAttribute4RelationTypeCollection.metadataActions.Add(ActionType.GetAccess, false);
    DBAttribute4RelationTypeCollection.metadataActions.Add(ActionType.SetAccess, false);
    DBAttribute4RelationTypeCollection.metadataActions.Add(ActionType.EditProperties, false);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBAttribute4RelationTypeCollection.metadataActions);
  }

  public override bool AnyAttributes
  {
    get => this.UserSession.GetRelationType((int) this.ParentID).AnyAttributes;
  }

  public override AttributeSourceTypes CollectionSourceType => AttributeSourceTypes.Relation;

  public override DataTable Select(string orderBy, params object[] addInfo)
  {
    if (addInfo != null)
    {
      foreach (object obj in addInfo)
      {
        if (obj is string && obj.ToString() == "ALL_FIELDS")
        {
          this._DBTableName = "IMS_ATTR4RELTYPE_VIEW";
          this._SelectFromCache = false;
        }
      }
    }
    DataTable dataTable = base.Select(orderBy, addInfo);
    if (this._ftFilter != FieldTypes.ftUnknown && this._DBTableName == "IMS_ATTR4RELATION_TYPES")
    {
      for (int index = dataTable.Rows.Count - 1; index >= 0; --index)
      {
        if (this._ftFilter != this.UserSession.GetAttributeType(Convert.ToInt32(dataTable.Rows[index]["F_ATTRIBUTE_ID"])).AttributeType)
          dataTable.Rows.RemoveAt(index);
      }
      dataTable.AcceptChanges();
      this._ftFilter = FieldTypes.ftUnknown;
    }
    return dataTable;
  }

  public override string ObjectName
  {
    get
    {
      IDBRelationType relationType = this.UserSession.GetRelationType((int) this.ParentID);
      return string.Format(LocalizationHolder.rm.GetString("Kernel_873"), (object) relationType.Description);
    }
  }

  public override object ParentID
  {
    get => base.ParentID;
    set
    {
      if (this.ParentID == value)
        return;
      if (this.UserSession.DBCache.GetTable("IMS_RELATION_TYPES").Rows.Find(value) == null)
        throw new KernelExceptionID(sc_12431.ssp_appserver_12455(486421843), value);
      base.ParentID = value;
    }
  }

  protected override string GetParentSQL(object parentID)
  {
    return this._ftFilter != FieldTypes.ftUnknown ? $"(F_RELATION_TYPE = {parentID} AND F_ATTRIBUTE_TYPE = {(int) this._ftFilter})" : $"(F_RELATION_TYPE = {parentID.ToString()})";
  }

  protected override IDBAttributeType GetAttributeType4(int attrID, bool failIfNotFound)
  {
    return (IDBAttributeType) this.GetAttributeByID(attrID, false) ?? this.UserSession.GetAttributeType(attrID, failIfNotFound);
  }

  public override IDBAttributeType4 GetAttributeByID(int attributeID, bool throwNotFoundException)
  {
    IDBAttributeType4 attributeById1;
    if (this.Attributes.TryGetValue(attributeID, out attributeById1))
      return attributeById1;
    DataRow row = this.UserSession.DBCache.GetTable("IMS_ATTR4RELATION_TYPES").Rows.Find(new object[2]
    {
      (object) (int) this.ParentID,
      (object) attributeID
    });
    if (row != null)
    {
      IDBAttributeType4 attributeById2;
      switch (Convert.ToInt32(this.UserSession.DBCache.GetTable("IMS_ATTRIBUTES").Rows.Find((object) attributeID)["F_ATTRIBUTE_TYPE"]))
      {
        case 8:
          attributeById2 = (IDBAttributeType4) new DBObjectLinkAttributeType4Relation(this.UserSession, row);
          break;
        case 13:
          attributeById2 = (IDBAttributeType4) new DBMeasureAttributeType4Relation(this.UserSession, row);
          break;
        case 17:
          attributeById2 = (IDBAttributeType4) new DBObjectLinkByIDAttributeType4Relation(this.UserSession, row);
          break;
        default:
          attributeById2 = (IDBAttributeType4) new DBAttributeType4Relation(this.UserSession, row);
          break;
      }
      this.Attributes.Add(attributeID, attributeById2);
      return attributeById2;
    }
    if (throwNotFoundException)
      throw new AttributeTypeNotFoundException(string.Format(LocalizationHolder.rm.GetString(sc_12431.ssp_appserver_12456()), (object) this.UserSession.GetRelationType((int) this.ParentID).Description, (object) this.UserSession.GetAttributeType(attributeID).Name));
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
      throw new AttributeTypeNotFoundException(string.Format(LocalizationHolder.rm.GetString(sc_12431.ssp_appserver_12457()), (object) attributeName));
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
      throw new AttributeTypeNotFoundException(string.Format(LocalizationHolder.rm.GetString(sc_12431.ssp_appserver_12458()), (object) attributeGuid));
    return (IDBAttributeType4) null;
  }

  public IDBAttributeType4 GetAttributeByGUID(Guid attributeGuid)
  {
    return this.GetAttributeByGUID(attributeGuid, false);
  }

  public IDBAttributeType4Relation Create(Attribute4RelationTypeProperties attrProperties)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    IDBAttributeType attributeType = this.UserSession.GetAttributeType(attrProperties.AttributeID);
    IDBRelationType relationType = this.UserSession.GetRelationType((int) this.ParentID);
    this._LastEventID = (relationType as DBRelationType).AddEvent(0L, ActionType.EditProperties, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString("Kernel_150"), (object) attributeType.Name));
    this.UserSession.StartTransaction();
    try
    {
      this.CheckAccess(ActionType.EditProperties);
      if (attributeType is DBSystemAttributeType)
        throw new KernelExceptionID(sc_12431.ssp_appserver_12459(1128632918));
      if (this.UserSession.DBCache.GetTable(this._DBTableName).Rows.Find(new object[2]
      {
        this.ParentID,
        (object) attrProperties.AttributeID
      }) != null)
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12431.ssp_appserver_12460()), (object) attributeType.Name, (object) relationType.Description));
      dataManager.ExecuteNonQuery(string.Format(sc_12431.ssp_appserver_12461(), (object) attrProperties.AttributeID, this.ParentID, (object) Convert.ToInt32((object) RequiredModes.Manual)));
      DataTable dataTable = dataManager.ExecuteDataTable(string.Format(sc_12431.ssp_appserver_12462(), (object) attrProperties.AttributeID, this.ParentID));
      if (dataTable.Rows.Count <= 0)
        throw new KernelExceptionID(sc_12431.ssp_appserver_12463(321147179));
      this.UserSession.DBCache.AddRow(this._DBTableName, dataTable.Rows[0], (IUserSession) this.UserSession);
      IDBAttributeType4Relation attributeById = this.GetAttributeByID(attrProperties.AttributeID) as IDBAttributeType4Relation;
      (attributeById as DBSessionable).LoggingOn = false;
      attributeById.ValidationRule = attrProperties.ValidationRule;
      attributeById.Computed = attrProperties.ComputeValueMode;
      attributeById.Formula = attrProperties.Formula;
      if (attrProperties.DefaultValue == null)
        attributeById.DefaultValue = (object) null;
      else
        attributeById.DefaultValue = (object) Convert.ToString(attrProperties.DefaultValue, (IFormatProvider) CultureInfo.InvariantCulture);
      attributeById.OptimizationMode = attrProperties.OptimizationMode;
      attributeById.Required = attrProperties.RequiredMode;
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
      string str = string.Format(LocalizationHolder.rm.GetString(sc_12431.ssp_appserver_12464()), (object) attributeType.Name, (object) ex.Message);
      this.UserSession.Rollback();
      this.CloseEvent(this._LastEventID, EventlogRecordType.Error, str);
      throw new KernelException(str, ex);
    }
  }
}
