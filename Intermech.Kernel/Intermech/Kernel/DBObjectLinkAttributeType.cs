// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBObjectLinkAttributeType
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

internal class DBObjectLinkAttributeType : DBObjectLinkBaseAttributeType, IDBObjectLinkAttributeType
{
  public DBObjectLinkAttributeType(UserSession uSession, DataRow attributeRow)
    : base(uSession, attributeRow)
  {
    AttributeCacheHelper.GetAttributeTypeValues(FieldTypes.ftObjectLink, this._AttributeID, ref this._ValueFieldName, ref this._TextFieldName, ref this._ConvertList, ref this._EnabledOperators, ref this._ComputableAttribute, ref this._PossibleValueFieldName);
    this._UniquedAttribute = true;
    this.CompatibleTypes = new FieldTypes[2]
    {
      FieldTypes.ftInteger,
      FieldTypes.ftObjectLink
    };
  }

  public override void ValidateDefaultValue(object newValue)
  {
    if (newValue == null || !(newValue.ToString() != string.Empty) || !(newValue.ToString() != "0") || !(newValue.ToString() != Consts.CurrentUserFunction))
      return;
    IDBObject dbObject = this.UserSession.GetObject(Convert.ToInt64(newValue));
    if (this.SizeType > 0L && (long) dbObject.ObjectType != this.SizeType)
    {
      for (int objectTypeParentId = this.UserSession.DBCache.GetObjectTypeParentID(dbObject.ObjectType); objectTypeParentId > -1; objectTypeParentId = this.UserSession.DBCache.GetObjectTypeParentID(objectTypeParentId))
      {
        if ((long) objectTypeParentId == this.SizeType)
          return;
      }
      IDBObjectType objectType = this.UserSession.GetObjectType(Convert.ToInt32(this.SizeType));
      throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_12702.ssp_appserver_12703()), (object) objectType.ObjectTypeName));
    }
  }

  public override string DefaultValueDescription
  {
    get
    {
      if (this.DefaultValue == null || this.DefaultValue != null && (this.DefaultValue == DBNull.Value || this.DefaultValue.ToString() == string.Empty))
        return base.DefaultValueDescription;
      try
      {
        return this.UserSession.DBCache.GetObjectInfo(this.UserSession.DataManager, Convert.ToInt64(this.DefaultValue)).Caption;
      }
      catch
      {
        return base.DefaultValueDescription;
      }
    }
  }

  protected override void ValidateChangeAttributeType(FieldTypes newType)
  {
    base.ValidateChangeAttributeType(newType);
    string linksCaption = this.GetLinksCaption("F_MASTER_ID");
    if (linksCaption != string.Empty)
      throw new KernelExceptionID(sc_12702.ssp_appserver_12704(298512862), (object) this.Name, (object) linksCaption);
    IDbManager dataManager = this.UserSession.DataManager;
    IDbDataParameter dbDataParameter = dataManager.Parameter("attrID", (object) this.AttributeID);
    List<string> objectAttrsTables = this.UserSession.DBCache.GetObjectAttrsTables();
    for (int index = 0; index < objectAttrsTables.Count; ++index)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable($"SELECT * FROM {objectAttrsTables[index]} WHERE (F_ATTRIBUTE_ID = :attrID) AND (F_INTEGER_VALUE IS NOT NULL)", dbDataParameter).Rows)
      {
        long int64 = Convert.ToInt64(row["F_OBJECT_ID"]);
        dataManager.ExecuteNonQuery("DELETE FROM IMS_OBJECT_LINKS WHERE F_OBJECT_ID = :objID AND F_ATTRIBUTE_ID = :attrID AND F_INLIST_ID = :inlistID", dataManager.Parameter("objID", (object) int64), dbDataParameter, dataManager.Parameter("inlistID", (object) Convert.ToInt32(row["F_INLIST_ID"])));
        if (newType == FieldTypes.ftObjectLinkByID)
        {
          IDBObject dbObject1 = this.UserSession.GetObject(int64, false);
          if (dbObject1 != null)
          {
            IDBObject dbObject2 = this.UserSession.GetObject(Convert.ToInt64(row["F_INTEGER_VALUE"]), false);
            string caption = dbObject2.Caption;
            long id = dbObject2.ID;
            if (!dbObject2.IsBaseVersion)
            {
              IDBObject objectBaseVersionById = this.UserSession.GetObjectBaseVersionByID(id, false);
              if (objectBaseVersionById != null)
                caption = objectBaseVersionById.Caption;
            }
            DBAdditionalAttribute attributeById = dbObject1.GetAttributeByID(this.AttributeID) as DBAdditionalAttribute;
            attributeById.Index = Convert.ToInt32(row["F_INLIST_ID"]);
            attributeById.DirectSetValue("F_INTEGER_VALUE", (object) id);
            if (caption.Trim() != row["F_STRING_VALUE"].ToString())
              attributeById.DirectSetValue("F_STRING_VALUE", (object) caption);
            dataManager.ExecuteNonQuery("INSERT INTO IMS_ID_LINKS (F_OBJECT_ID, F_ATTRIBUTE_ID, F_INLIST_ID, F_TO_ID) VALUES (:objID, :attrID, :inlistID, :newID1)", dataManager.Parameter("objID", (object) int64), dbDataParameter, dataManager.Parameter("inlistID", (object) Convert.ToInt32(row["F_INLIST_ID"])), dataManager.Parameter("newID1", (object) id));
          }
        }
      }
    }
    if (newType == FieldTypes.ftObjectLinkByID)
    {
      foreach (DataRow row in (InternalDataCollectionBase) dataManager.ExecuteDataTable("SELECT * FROM IMS_RELATION_ATTRS WHERE (F_ATTRIBUTE_ID = :attrID) AND (F_INTEGER_VALUE IS NOT NULL)", dbDataParameter).Rows)
      {
        IDBRelation relation = this.UserSession.GetRelation(Convert.ToInt64(row["F_PRJLINK_ID"]), false);
        if (relation != null)
        {
          IDBObject dbObject = this.UserSession.GetObject(Convert.ToInt64(row["F_INTEGER_VALUE"]), false);
          string caption = dbObject.Caption;
          long id = dbObject.ID;
          if (!dbObject.IsBaseVersion)
          {
            IDBObject objectBaseVersionById = this.UserSession.GetObjectBaseVersionByID(id, false);
            if (objectBaseVersionById != null)
              caption = objectBaseVersionById.Caption;
          }
          DBAdditionalAttribute attributeById = relation.GetAttributeByID(this.AttributeID) as DBAdditionalAttribute;
          attributeById.Index = Convert.ToInt32(row["F_INLIST_ID"]);
          attributeById.DirectSetValue("F_INTEGER_VALUE", (object) id);
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
