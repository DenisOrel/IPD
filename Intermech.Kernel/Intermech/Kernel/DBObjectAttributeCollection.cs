// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBObjectAttributeCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using System;
using System.Data;


namespace Intermech.Kernel;

public class DBObjectAttributeCollection(
  UserSession uSession,
  long objectID,
  int objectType,
  IDBAttributable parent) : DBAttributeCollection(uSession, objectID, objectType, parent)
{
  protected override void InitSystemAttributes()
  {
    DBObject parent = this._Parent as DBObject;
    this._AttributesList.Add((DBAttribute) new DBObjectSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_OBJECT_ID, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBObjectSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_ID, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBObjectSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_LC_STEP, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBObjectSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_VERSION_ID, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBObjectSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_CHKOUT_BY, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBObjectSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_OBJECT_VER_TYPE, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBObjectSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_OBJECT_TYPE, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBObjectSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_OWNER_ID, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBObjectSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_MODIFY_DATE, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBObjectSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_LEVEL_ID, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBObjectSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_OBJ_CREATE, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBObjectSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_CREATOR_ID, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBObjectSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_PROJECT_ID, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBObjectSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_MODIFICATION_ID, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBObjectSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_BASE_VERSION, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBObjectSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_SITE_ID, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBObjectSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_ACCESS, (IDBAttributeCollection) this, parent));
  }

  protected override string AttributesTableName
  {
    get
    {
      return (Convert.ToInt32(this.UserSession.DBCache.GetTable("IMS_OBJECT_TYPES").Rows.Find((object) this.ObjectType)["F_OPTIONS"]) & 16 /*0x10*/) == 0 ? "IMS_OBJECT_ATTRS" : "IMV_A" + this.ObjectType.ToString();
    }
  }

  protected override string AttributesKeyName => "F_OBJECT_ID";

  protected override IDBAttribute DoAddAttribute(int attributeID, bool checkEnabled)
  {
    if (checkEnabled)
    {
      IDBObjectType objectType = this.UserSession.GetObjectType(this.ObjectType);
      IDBAttributeType dbAttributeType = (IDBAttributeType) objectType.Attributes.GetAttributeByID(attributeID, false);
      if (!objectType.AnyAttributes && dbAttributeType == null)
        throw new KernelExceptionID(sc_12542.ssp_appserver_12543(1115231566), (object) objectType.ObjectTypeName, (object) this.UserSession.GetAttributeType(attributeID).Name);
      AttributeOptions options;
      if (dbAttributeType == null)
      {
        dbAttributeType = this.UserSession.GetAttributeType(attributeID);
        options = dbAttributeType.Options;
      }
      else
        options = this.UserSession.GetAttributeType(attributeID).Options;
      if ((options & AttributeOptions.LocalImbaseAttribute) == AttributeOptions.LocalImbaseAttribute)
        throw new KernelExceptionID(sc_12542.ssp_appserver_12544(191301975), (object) dbAttributeType.Name);
      (this._Parent as DBObject).CheckEditMode((dbAttributeType.Options & AttributeOptions.ModifyInBase) == AttributeOptions.None, dbAttributeType.IsContent, false);
    }
    object obj;
    if (this.CheckExistMode)
      obj = this.UserSession.DataManager.ExecuteScalar($"SELECT F_OBJECT_ID FROM {this.AttributesTableName} WHERE F_ATTRIBUTE_ID = :attrID AND F_OBJECT_ID = :objID AND F_INLIST_ID = 0", this.UserSession.DataManager.Parameter("attrID", (object) attributeID), this.UserSession.DataManager.Parameter("objID", (object) this.ObjectID));
    else
      obj = (object) null;
    if (obj == null || obj == DBNull.Value)
      this.UserSession.DataManager.ExecuteNonQuery($"INSERT INTO {this.AttributesTableName} (F_ATTRIBUTE_ID, F_OBJECT_ID, F_INLIST_ID) VALUES (:aID, :oID, 0)", this.UserSession.DataManager.Parameter("aID", (object) attributeID), this.UserSession.DataManager.Parameter("oID", (object) this.ObjectID));
    DataTable objectAttsEmptyRow = this.UserSession.DBCache.GetObjectAttsEmptyRow(attributeID, this.ObjectID, 0);
    if (DBAttributeCollection.AttributeCreatorService == null)
      DBAttributeCollection.AttributeCreatorService = ServerServices.GetService(typeof (IDBAttributeService)) as IDBAttributeService;
    return DBAttributeCollection.AttributeCreatorService.CreateAttribute((IUserSession) this.UserSession, objectAttsEmptyRow, 0, false, this._Parent);
  }

  internal override string[] GetUpdateTables(int attrID)
  {
    return this.UserSession.DBCache.GetUpdateTables(attrID, this.ObjectType, -1);
  }

  protected override IDBAttribute DoAddTemporaryAttribute(int attributeID)
  {
    DataTable objectAttsEmptyRow = this.UserSession.DBCache.GetObjectAttsEmptyRow(attributeID, this.ObjectID, 0);
    if (DBAttributeCollection.AttributeCreatorService == null)
      DBAttributeCollection.AttributeCreatorService = ServerServices.GetService(typeof (IDBAttributeService)) as IDBAttributeService;
    return DBAttributeCollection.AttributeCreatorService.CreateAttribute((IUserSession) this.UserSession, objectAttsEmptyRow, 0, true, this._Parent);
  }

  protected override IDBAttributableType ParentType
  {
    get => (IDBAttributableType) (this._Parent as DBObject).ObjectTypeClass;
  }
}
