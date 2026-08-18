// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBAHistoryCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Data;
using System.Diagnostics;


namespace Intermech.Kernel;

public class DBAHistoryCollection : DBRecordSet, IDBAHistoryCollection, IDBRecords, IDBSessionable
{
  private IDBAttributeType _Attribute;

  public DBAHistoryCollection(UserSession uSession, int attributeID)
    : base(uSession, attributeID)
  {
    this._DBObjectTableName = "IMS_ATTR_HISTORY";
    this._DBKeyField = "F_KEY";
    this._DBKeyFieldID = Convert.ToInt32((object) ObligatoryObjectAttributes.F_KEY);
    this._DBAttributesTableName = "IMS_OBJECT_ATTRS";
    this._Attribute = this.UserSession.GetAttributeType(attributeID, true);
    (this._Attribute as IDBSecurity).CheckAccess(ActionType.List, true);
  }

  protected override AttributeSourceTypes AutoAttributeSourceTypes
  {
    [DebuggerStepThrough] get => AttributeSourceTypes.History;
  }

  public int TextFieldID
  {
    get
    {
      return (int) ObligatoryObjectAttributesHelper.GetObligatoryObjectAttribute(this._Attribute.TextFieldName);
    }
  }

  public int ValueFieldID
  {
    get
    {
      return (int) ObligatoryObjectAttributesHelper.GetObligatoryObjectAttribute(this._Attribute.ValueFieldName);
    }
  }

  public IDBAttributeType GetAttributeTypeByID(long id, AttributeSourceTypes st)
  {
    IDBAttributeType dbAttributeType = (IDBAttributeType) null;
    IDbManager dataManager = this.UserSession.DataManager;
    if (st == AttributeSourceTypes.Relation)
    {
      object obj = dataManager.ExecuteScalar("SELECT F_RELATION_TYPE FROM IMS_RELATIONS WHERE F_PRJLINK_ID = :id1", dataManager.Parameter("id1", (object) id));
      if (obj == null || obj == DBNull.Value)
        throw new KernelExceptionID(sc_12572.ssp_appserver_12573(2816438), (object) id);
      dbAttributeType = (IDBAttributeType) this.UserSession.GetRelationType(Convert.ToInt32(obj)).Attributes.GetAttributeByID(this._Attribute.AttributeID, false);
    }
    else
    {
      QuickObjectInfo objectInfo = this.UserSession.DBCache.GetObjectInfo(dataManager, id);
      if (!objectInfo.Empty)
        dbAttributeType = (IDBAttributeType) this.UserSession.GetObjectType(objectInfo.ObjectTypeID).Attributes.GetAttributeByID(this._Attribute.AttributeID, false);
    }
    return dbAttributeType ?? this._Attribute;
  }

  public void DeleteHistory(long id, AttributeSourceTypes st)
  {
    IDBAttributeType attributeTypeById = this.GetAttributeTypeByID(id, st);
    IDbManager dataManager = this.UserSession.DataManager;
    if ((attributeTypeById.Options & AttributeOptions.SaveCommonHistory) == AttributeOptions.SaveCommonHistory)
      dataManager.ExecuteNonQuery("UPDATE IMS_ATTR_HISTORY SET F_STATUS = 1 WHERE F_ATTRIBUTE_ID = :aID1 AND F_USER_ID = :uID1 AND F_ID = :id1 AND F_STATUS = 0", dataManager.Parameter("aID1", (object) this._Attribute.AttributeID), dataManager.Parameter("uID1", (object) this.UserSession.UserID), dataManager.Parameter("id1", (object) id));
    else
      dataManager.ExecuteNonQuery("DELETE FROM IMS_ATTR_HISTORY WHERE F_ATTRIBUTE_ID = :aID1 AND F_USER_ID = :uID1 AND F_ID = :id1", dataManager.Parameter("aID1", (object) this._Attribute.AttributeID), dataManager.Parameter("uID1", (object) this.UserSession.UserID), dataManager.Parameter("id1", (object) id));
  }

  public void DeleteHistory4Type(int typeID, AttributeSourceTypes st)
  {
    IDBAttributeType attribute = this._Attribute;
    IDBAttributeType attributeById;
    switch (st)
    {
      case AttributeSourceTypes.Object:
        attributeById = (IDBAttributeType) this.UserSession.GetObjectType(typeID).Attributes.GetAttributeByID(this._Attribute.AttributeID, false);
        break;
      case AttributeSourceTypes.Relation:
        attributeById = (IDBAttributeType) this.UserSession.GetRelationType(typeID).Attributes.GetAttributeByID(this._Attribute.AttributeID, false);
        break;
      default:
        return;
    }
    string empty = string.Empty;
    string str = st != AttributeSourceTypes.Relation ? "F_OBJECT_TYPE" : "F_RELATION_TYPE";
    if (!(str != string.Empty))
      return;
    IDbManager dataManager = this.UserSession.DataManager;
    if ((attributeById.Options & AttributeOptions.SaveCommonHistory) == AttributeOptions.SaveCommonHistory)
      dataManager.ExecuteNonQuery(string.Format(sc_12572.ssp_appserver_12574(), (object) str), dataManager.Parameter("aID1", (object) this._Attribute.AttributeID), dataManager.Parameter("uID1", (object) this.UserSession.UserID), dataManager.Parameter("ot", (object) typeID));
    else
      dataManager.ExecuteNonQuery(string.Format(sc_12572.ssp_appserver_12575(), (object) str), dataManager.Parameter("aID1", (object) this._Attribute.AttributeID), dataManager.Parameter("uID1", (object) this.UserSession.UserID), dataManager.Parameter("ot", (object) typeID));
  }

  public void DeleteHistory(AttributeSourceTypes st)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    if (st == AttributeSourceTypes.Object)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable(sc_12572.ssp_appserver_12576(), dataManager.Parameter("aID1", (object) this._Attribute.AttributeID), dataManager.Parameter("uID1", (object) this.UserSession.UserID)).Rows)
      {
        IDBObjectType objectType = this.UserSession.GetObjectType(Convert.ToInt32(row[0]), false);
        if (objectType != null && (((IDBAttributeType) objectType.Attributes.GetAttributeByID(this._Attribute.AttributeID, false) ?? this._Attribute).Options & AttributeOptions.SaveCommonHistory) == AttributeOptions.SaveCommonHistory)
          dataManager.ExecuteNonQuery("UPDATE IMS_ATTR_HISTORY SET F_STATUS = 1 WHERE F_STATUS = 0 AND F_ATTRIBUTE_ID = :aID1 AND F_USER_ID = :uID1 AND F_OBJECT_TYPE = :ot", dataManager.Parameter("aID1", (object) this._Attribute.AttributeID), dataManager.Parameter("uID1", (object) this.UserSession.UserID), dataManager.Parameter("ot", (object) objectType.ObjectType));
        else
          dataManager.ExecuteNonQuery("DELETE FROM IMS_ATTR_HISTORY WHERE F_ATTRIBUTE_ID = :aID1 AND F_USER_ID = :uID1 AND F_OBJECT_TYPE = :ot", dataManager.Parameter("aID1", (object) this._Attribute.AttributeID), dataManager.Parameter("uID1", (object) this.UserSession.UserID), dataManager.Parameter("ot", (object) Convert.ToInt32(row[0])));
      }
    }
    else
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable(sc_12572.ssp_appserver_12577(), dataManager.Parameter("aID1", (object) this._Attribute.AttributeID), dataManager.Parameter("uID1", (object) this.UserSession.UserID)).Rows)
      {
        IDBRelationType relationType = this.UserSession.GetRelationType(Convert.ToInt32(row[0]), false);
        if (relationType != null && (((IDBAttributeType) relationType.Attributes.GetAttributeByID(this._Attribute.AttributeID, false) ?? this._Attribute).Options & AttributeOptions.SaveCommonHistory) == AttributeOptions.SaveCommonHistory)
          dataManager.ExecuteNonQuery("UPDATE IMS_ATTR_HISTORY SET F_STATUS = 1 WHERE F_STATUS = 0 AND F_ATTRIBUTE_ID = :aID1 AND F_USER_ID = :uID1 AND F_RELATION_TYPE = :ot", dataManager.Parameter("aID1", (object) this._Attribute.AttributeID), dataManager.Parameter("uID1", (object) this.UserSession.UserID), dataManager.Parameter("ot", (object) relationType.RelationType));
        else
          dataManager.ExecuteNonQuery("DELETE FROM IMS_ATTR_HISTORY WHERE F_ATTRIBUTE_ID = :aID1 AND F_USER_ID = :uID1 AND F_RELATION_TYPE = :ot", dataManager.Parameter("aID1", (object) this._Attribute.AttributeID), dataManager.Parameter("uID1", (object) this.UserSession.UserID), dataManager.Parameter("ot", (object) Convert.ToInt32(row[0])));
      }
    }
  }

  public override string ObjectName
  {
    get
    {
      return string.Format(LocalizationHolder.rm.GetString("Kernel_1"), (object) this._Attribute.Name);
    }
  }

  protected override IDBAttributeType[] GetColumnsCollection(
    ref DBRecordSetParams pars,
    bool failIfNotFound)
  {
    if (pars.Columns == null)
    {
      if (this._Attribute.TextFieldName != this._Attribute.ValueFieldName)
        pars.Columns = new object[2]
        {
          (object) ObligatoryObjectAttributesHelper.GetObligatoryObjectAttribute(this._Attribute.TextFieldName),
          (object) ObligatoryObjectAttributesHelper.GetObligatoryObjectAttribute(this._Attribute.ValueFieldName)
        };
      else
        pars.Columns = new object[1]
        {
          (object) ObligatoryObjectAttributesHelper.GetObligatoryObjectAttribute(this._Attribute.TextFieldName)
        };
    }
    return base.GetColumnsCollection(ref pars, failIfNotFound);
  }

  protected override void ConfigureQueryBuilder(ConditionStructure[] conditions)
  {
    base.ConfigureQueryBuilder(conditions);
  }

  public override DataTable Select(DBRecordSetParams paramSet)
  {
    this.UserSession.QueryBuilder.SystemTableName = "IMS_ATTR_HISTORY";
    int num = -1;
    if (paramSet.Columns != null)
    {
      for (int index = 0; index < paramSet.Columns.Length; ++index)
      {
        if (paramSet.Columns[index] is ObligatoryObjectAttributes && (ObligatoryObjectAttributes) paramSet.Columns[index] == ObligatoryObjectAttributes.F_USER_ID)
        {
          num = index;
          break;
        }
        if (paramSet.Columns[index] is int && (int) paramSet.Columns[index] == -36)
        {
          num = index;
          break;
        }
      }
    }
    DataTable dataTable = base.Select(paramSet);
    if (num >= 0 && dataTable.Rows.Count > 0 && paramSet.Tags != null && paramSet.Tags.Contains((object) "UserCaptions"))
    {
      dataTable.Columns.Add("USER_CAPTION", typeof (string));
      foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      {
        QuickObjectInfo objectInfo = this.UserSession.GetObjectInfo(Convert.ToInt64(row[num]));
        if (!objectInfo.Empty)
          row[dataTable.Columns.Count - 1] = (object) objectInfo.Caption;
        else
          row[dataTable.Columns.Count - 1] = (object) string.Empty;
      }
      string columnName = dataTable.Columns[num].ColumnName;
      dataTable.Columns.RemoveAt(num);
      dataTable.Columns[dataTable.Columns.Count - 1].ColumnName = columnName;
    }
    return dataTable;
  }
}
