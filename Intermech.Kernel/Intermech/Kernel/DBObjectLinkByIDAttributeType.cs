// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBObjectLinkByIDAttributeType
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel;

internal class DBObjectLinkByIDAttributeType : 
  DBObjectLinkBaseAttributeType,
  IDBObjectLinkByIDAttributeType,
  IDBObjectLinkAttributeType
{
  public DBObjectLinkByIDAttributeType(UserSession uSession, DataRow attributeRow)
    : base(uSession, attributeRow)
  {
    AttributeCacheHelper.GetAttributeTypeValues(FieldTypes.ftObjectLinkByID, this._AttributeID, ref this._ValueFieldName, ref this._TextFieldName, ref this._ConvertList, ref this._EnabledOperators, ref this._ComputableAttribute, ref this._PossibleValueFieldName);
    this._UniquedAttribute = true;
    this.CompatibleTypes = new FieldTypes[3]
    {
      FieldTypes.ftInteger,
      FieldTypes.ftObjectLink,
      FieldTypes.ftObjectLinkByID
    };
  }

  public override void ValidateDefaultValue(object newValue)
  {
    if (newValue == null || !(newValue.ToString() != string.Empty) || !(newValue.ToString() != "0") || !(newValue.ToString() != Consts.CurrentUserFunction))
      return;
    IDBObject objectById = this.UserSession.GetObjectByID(Convert.ToInt64(newValue), true);
    if (this.SizeType > 0L && (long) objectById.ObjectType != this.SizeType)
    {
      for (int objectTypeParentId = this.UserSession.DBCache.GetObjectTypeParentID(objectById.ObjectType); objectTypeParentId > -1; objectTypeParentId = this.UserSession.DBCache.GetObjectTypeParentID(objectTypeParentId))
      {
        if ((long) objectTypeParentId == this.SizeType)
          return;
      }
      IDBObjectType objectType = this.UserSession.GetObjectType(Convert.ToInt32(this.SizeType));
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12709.ssp_appserver_12710()), (object) objectType.ObjectTypeName));
    }
  }

  public override string DefaultValueDescription
  {
    get
    {
      if (this.DefaultValue != null && (this.DefaultValue == null || this.DefaultValue != DBNull.Value && !(this.DefaultValue.ToString() == string.Empty)))
      {
        try
        {
          IDBObject objectById = this.UserSession.GetObjectByID(Convert.ToInt64(this.DefaultValue), false);
          if (objectById != null)
            return objectById.Caption;
        }
        catch
        {
        }
      }
      return base.DefaultValueDescription;
    }
  }

  protected override void ValidateChangeAttributeType(FieldTypes newType)
  {
    base.ValidateChangeAttributeType(newType);
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter = dataManager.Parameter("attrID", (object) this.AttributeID);
    List<string> objectAttrsTables = this.UserSession.DBCache.GetObjectAttrsTables();
    for (int index = 0; index < objectAttrsTables.Count; ++index)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable($"SELECT * FROM {objectAttrsTables[index]} WHERE (F_ATTRIBUTE_ID = :attrID) AND (F_INTEGER_VALUE IS NOT NULL)", dbDataParameter).Rows)
      {
        long int64 = Convert.ToInt64(row["F_OBJECT_ID"]);
        dataManager.ExecuteNonQuery("DELETE FROM IMS_ID_LINKS WHERE F_TO_ID = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :inlistID", dataManager.Parameter("objID", (object) int64), dbDataParameter, dataManager.Parameter("inlistID", (object) Convert.ToInt32(row["F_INLIST_ID"])));
        if (newType == FieldTypes.ftObjectLink)
        {
          IDBObject dbObject = this.UserSession.GetObject(int64, false);
          if (dbObject != null)
          {
            IDBObject objectBaseVersionById = this.UserSession.GetObjectBaseVersionByID(Convert.ToInt64(row["F_INTEGER_VALUE"]), false);
            string caption = objectBaseVersionById.Caption;
            DBAdditionalAttribute attributeById = dbObject.GetAttributeByID(this.AttributeID) as DBAdditionalAttribute;
            attributeById.Index = Convert.ToInt32(row["F_INLIST_ID"]);
            attributeById.DirectSetValue("F_INTEGER_VALUE", (object) Math.Abs(objectBaseVersionById.ObjectID));
            if (caption.Trim() != row["F_STRING_VALUE"].ToString())
              attributeById.DirectSetValue("F_STRING_VALUE", (object) caption);
            dataManager.ExecuteNonQuery("INSERT INTO IMS_OBJECT_LINKS (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_TOOBJECT_ID) VALUES (:objID, :attrID, :inlistID, :newID1)", dataManager.Parameter("objID", (object) int64), dbDataParameter, dataManager.Parameter("inlistID", (object) Convert.ToInt32(row["F_INLIST_ID"])), dataManager.Parameter("newID1", (object) objectBaseVersionById.ObjectID));
          }
        }
      }
    }
    if (newType == FieldTypes.ftObjectLink)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable("SELECT * FROM IMS_RELATION_ATTRS WHERE (F_ATTRIBUTE_ID = :attrID) AND (F_INTEGER_VALUE IS NOT NULL)", dbDataParameter).Rows)
      {
        IDBRelation relation = this.UserSession.GetRelation(Convert.ToInt64(row["F_PRJLINK_ID"]), false);
        if (relation != null)
        {
          IDBObject objectBaseVersionById = this.UserSession.GetObjectBaseVersionByID(Convert.ToInt64(row["F_INTEGER_VALUE"]), false);
          string caption = objectBaseVersionById.Caption;
          DBAdditionalAttribute attributeById = relation.GetAttributeByID(this.AttributeID) as DBAdditionalAttribute;
          attributeById.Index = Convert.ToInt32(row["F_INLIST_ID"]);
          attributeById.DirectSetValue("F_INTEGER_VALUE", (object) Math.Abs(objectBaseVersionById.ObjectID));
          if (caption.Trim() != row["F_STRING_VALUE"].ToString())
            attributeById.DirectSetValue("F_STRING_VALUE", (object) caption);
        }
      }
    }
    if (newType == FieldTypes.ftString)
    {
      this.ClearValues("F_INTEGER_VALUE");
    }
    else
    {
      if (newType != FieldTypes.ftInteger)
        return;
      this.ClearValues("F_STRING_VALUE");
    }
  }
}
