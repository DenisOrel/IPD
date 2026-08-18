// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBRelationAttributeCollection
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

public class DBRelationAttributeCollection(
  UserSession uSession,
  long relationID,
  int relationType,
  IDBAttributable parent) : DBAttributeCollection(uSession, relationID, relationType, parent)
{
  protected override void InitSystemAttributes()
  {
    IDBRelation parent = this._Parent as IDBRelation;
    this._AttributesList.Add((DBAttribute) new DBRelationSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_PRJLINK_ID, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBRelationSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_PROJ_ID, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBRelationSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_PART_ID, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBRelationSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_RELATION_TYPE, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBRelationSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_CREATE_DATE, (IDBAttributeCollection) this, parent));
    this._AttributesList.Add((DBAttribute) new DBRelationSystemAttribute(this.UserSession, ObligatoryObjectAttributes.F_PRJ_GUID, (IDBAttributeCollection) this, parent));
  }

  protected override string AttributesTableName => "IMS_RELATION_ATTRS";

  protected override string AttributesKeyName => "F_PRJLINK_ID";

  protected override IDBAttribute DoAddAttribute(int attributeID, bool checkEnabled)
  {
    if (checkEnabled)
    {
      IDBRelationType relationType = this.UserSession.GetRelationType(this.ObjectType);
      IDBAttributeType dbAttributeType = (IDBAttributeType) relationType.Attributes.GetAttributeByID(attributeID, false);
      if (!relationType.AnyAttributes && dbAttributeType == null)
        throw new KernelExceptionID(sc_12552.ssp_appserver_12553(1584613078), (object) relationType.Description, (object) this.UserSession.GetAttributeType(attributeID).Name);
      AttributeOptions options;
      if (dbAttributeType == null)
      {
        dbAttributeType = this.UserSession.GetAttributeType(attributeID);
        options = dbAttributeType.Options;
      }
      else
        options = this.UserSession.GetAttributeType(attributeID).Options;
      if ((options & AttributeOptions.LocalImbaseAttribute) == AttributeOptions.LocalImbaseAttribute)
        throw new KernelExceptionID(sc_12552.ssp_appserver_12554(262296634), (object) dbAttributeType.Name);
      if (dbAttributeType.IsContent)
        (this._Parent as DBRelation).ValidateEditRelation(true);
    }
    object obj;
    if (this.CheckExistMode)
      obj = this.UserSession.DataManager.ExecuteScalar("SELECT F_OBJECT_ID FROM IMS_RELATION_ATTRS WHERE F_ATTRIBUTE_ID = :attrID AND F_PRJLINK_ID = :relID AND F_INLIST_ID = 0", this.UserSession.DataManager.Parameter("attrID", (object) attributeID), this.UserSession.DataManager.Parameter("relID", (object) this.ObjectID));
    else
      obj = (object) null;
    if (obj == null || obj == DBNull.Value)
      this.UserSession.DataManager.ExecuteNonQuery("INSERT INTO IMS_RELATION_ATTRS (F_ATTRIBUTE_ID, F_PRJLINK_ID, F_INLIST_ID) VALUES (:aID, :oID, 0)", this.UserSession.DataManager.Parameter("aID", (object) attributeID), this.UserSession.DataManager.Parameter("oID", (object) this.ObjectID));
    DataTable relationAttsEmptyRow = this.UserSession.DBCache.GetRelationAttsEmptyRow(attributeID, this.ObjectID, 0);
    if (DBAttributeCollection.AttributeCreatorService == null)
      DBAttributeCollection.AttributeCreatorService = ServerServices.GetService(typeof (IDBAttributeService)) as IDBAttributeService;
    return DBAttributeCollection.AttributeCreatorService.CreateAttribute((IUserSession) this.UserSession, relationAttsEmptyRow, 0, false, this._Parent);
  }

  internal override string[] GetUpdateTables(int attrID)
  {
    return this.UserSession.DBCache.GetUpdateTables(attrID, -1, this.ObjectType);
  }

  protected override IDBAttribute DoAddTemporaryAttribute(int attributeID)
  {
    DataTable relationAttsEmptyRow = this.UserSession.DBCache.GetRelationAttsEmptyRow(attributeID, this.ObjectID, 0);
    if (DBAttributeCollection.AttributeCreatorService == null)
      DBAttributeCollection.AttributeCreatorService = ServerServices.GetService(typeof (IDBAttributeService)) as IDBAttributeService;
    return DBAttributeCollection.AttributeCreatorService.CreateAttribute((IUserSession) this.UserSession, relationAttsEmptyRow, 0, true, this._Parent);
  }

  protected override IDBAttributableType ParentType
  {
    get => (IDBAttributableType) (this._Parent as DBRelation).RelationTypeObject;
  }

  public IDBAttributable Parent => this._Parent;
}
