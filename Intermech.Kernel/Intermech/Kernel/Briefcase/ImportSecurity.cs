// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportSecurity
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Globalization;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportSecurity : ImportItem
{
  private readonly int _categoryID = -1;
  private readonly List<IDСorresponds> _importingObjects;
  private readonly ArrayList _importingSecObj;
  private readonly Dictionary<long, string> _importingLCSteps;
  private readonly long _objectID = -1;
  private readonly string _objectGUID = string.Empty;
  public bool IsValid;

  public ImportSecurity(UserSession userSession)
    : base(userSession, (DataRow) null, (DataSet) null, ImportItemOptions.None)
  {
  }

  public ImportSecurity(
    UserSession userSession,
    DataRow briefRow,
    DataSet metaData,
    List<IDСorresponds> importingObjects,
    ArrayList importingSecObj,
    Dictionary<long, string> importingLCSteps,
    ImportItemOptions options)
    : base(userSession, briefRow, metaData, options)
  {
    this._importingObjects = importingObjects;
    this._importingSecObj = importingSecObj;
    this._importingLCSteps = importingLCSteps;
    try
    {
      Consts.InitCategoryNames();
      string str = string.Empty;
      this._categoryID = Convert.ToInt32(briefRow["F_CATEGORY_TYPE"]);
      switch (this._categoryID)
      {
        case 1:
          long catID = Convert.ToInt64(briefRow["F_CATEGORY_ID"]);
          IDСorresponds idСorresponds = catID != 0L ? this._importingObjects.Find((Predicate<IDСorresponds>) (x => x.SourceObjectID == catID)) : (IDСorresponds) null;
          if (idСorresponds != null)
          {
            IDBObject dbObject = this.session.GetObject(idСorresponds.HostObjectID, true);
            str = dbObject.Caption;
            this._objectID = dbObject.ObjectID;
            this._objectGUID = dbObject.GUID.ToString();
            break;
          }
          break;
        case 3:
          DataRow dataRow1 = metaData.Tables["IMS_ATTRIBUTES"].Rows.Find(briefRow["F_CATEGORY_ID"]);
          if (dataRow1 != null)
          {
            str = Convert.ToString(dataRow1["F_NAME"]);
            this._objectGUID = Convert.ToString(dataRow1["F_GUID"]);
            IDBAttributeType attributeType = this.session.GetAttributeType(new Guid(Convert.ToString(dataRow1["F_GUID"])), true);
            if (attributeType != null)
            {
              this._objectID = (long) attributeType.AttributeID;
              break;
            }
            break;
          }
          break;
        case 4:
          DataRow dataRow2 = metaData.Tables["IMS_OBJECT_TYPES"].Rows.Find(briefRow["F_CATEGORY_ID"]);
          if (dataRow2 != null)
          {
            str = Convert.ToString(dataRow2["F_OBJ_TYPE_NAME"]);
            this._objectGUID = Convert.ToString(dataRow2["F_GUID"]);
            IDBObjectType objectType = this.session.GetObjectType(new Guid(Convert.ToString(dataRow2["F_GUID"])), true);
            if (objectType != null)
            {
              this._objectID = (long) objectType.ObjectType;
              break;
            }
            break;
          }
          break;
        case 6:
          DataRow dataRow3 = metaData.Tables["IMS_RELATION_TYPES"].Rows.Find(briefRow["F_CATEGORY_ID"]);
          if (dataRow3 != null)
          {
            str = Convert.ToString(dataRow3["F_DESCRIPTION"]);
            this._objectGUID = Convert.ToString(dataRow3["F_GUID"]);
            IDBRelationType relationType = this.session.GetRelationType(new Guid(Convert.ToString(dataRow3["F_GUID"])), true);
            if (relationType != null)
            {
              this._objectID = (long) relationType.RelationType;
              break;
            }
            break;
          }
          break;
        case 7:
          long int64 = Convert.ToInt64(briefRow["F_CATEGORY_ID"]);
          int conformityObjectType = Helper.GetConformityObjectType((IUserSession) this.session, metaData.Tables["IMS_OBJECT_TYPES"], (int) (int64 >> 32 /*0x20*/ & (long) uint.MaxValue), true);
          int conformityLcStep = Helper.GetConformityLCStep(this.session, metaData.Tables["IMS_LC_STEPS"], (int) (int64 & (long) uint.MaxValue), true);
          if (conformityObjectType != -1 && conformityLcStep != -1)
          {
            IDBLifecycleStep lifecycleStep = this.session.GetLifecycleStep(conformityLcStep, true);
            IDBObjectType objectType = this.session.GetObjectType(conformityObjectType, true);
            str = string.Format(LocalizationHolder.rm.GetString("Kernel_316"), (object) lifecycleStep.LCName, (object) objectType.ObjectTypeName);
            this._objectID = Convert.ToInt64(conformityObjectType) << 32 /*0x20*/ | (long) conformityLcStep;
            if (!this._importingLCSteps.TryGetValue(this._objectID, out this._objectGUID))
            {
              this._objectGUID = Guid.NewGuid().ToString();
              this._importingLCSteps.Add(this._objectID, this._objectGUID);
              break;
            }
            break;
          }
          break;
        case 8:
          DataRow dataRow4 = metaData.Tables["IMS_LEVELS"].Rows.Find(briefRow["F_CATEGORY_ID"]);
          if (dataRow4 != null)
          {
            str = Convert.ToString(dataRow4["F_LEVEL_NAME"]);
            this._objectGUID = Convert.ToString(dataRow4["F_GUID"]);
            this._objectID = (long) this.session.GetLifecycleLevel(new Guid(Convert.ToString(dataRow4["F_GUID"])), true).LevelID;
            break;
          }
          break;
        case 9:
          str = LocalizationHolder.rm.GetString("Kernel_317");
          this._objectGUID = Consts.CategoryLanguageGUID.ToString();
          this._objectID = 0L;
          break;
        case 11:
          str = LocalizationHolder.rm.GetString("Kernel_318");
          this._objectID = 0L;
          this._objectGUID = Consts.CategorySubjectAreaGUID.ToString();
          break;
        case 12:
          DataRow dataRow5 = metaData.Tables["IMS_ATTR_GROUPS"].Rows.Find(briefRow["F_CATEGORY_ID"]);
          if (dataRow5 != null)
          {
            str = Convert.ToString(dataRow5["F_GROUP_NAME"]);
            this._objectGUID = Convert.ToString(dataRow5["F_GUID"]);
            this._objectID = (long) this.session.GetAttributesGroup(new Guid(Convert.ToString(dataRow5["F_GUID"])), true).GroupID;
            break;
          }
          break;
        case 16 /*0x10*/:
          DataRow dataRow6 = metaData.Tables["IMS_LC_SCHEMAS"].Rows.Find(briefRow["F_CATEGORY_ID"]);
          if (dataRow6 != null)
          {
            str = Convert.ToString(dataRow6["F_NAME"]);
            this._objectGUID = Convert.ToString(dataRow6["F_GUID"]);
            this._objectID = (long) this.session.GetLCSchema(new Guid(Convert.ToString(dataRow6["F_GUID"])), true).SchemaID;
            break;
          }
          break;
      }
      if (str == string.Empty)
        str = LocalizationHolder.rm.GetString("Kernel_319") + Convert.ToString(briefRow["F_CATEGORY_ID"]);
      this.UniIdentifiler = string.Format(LocalizationHolder.rm.GetString("Kernel_879"), (object) EnumDescConverter.GetEnumDescription((Enum) (ActionType) Convert.ToInt32(briefRow["F_RIGHT_ID"])), (object) str, (object) Consts.GetCategoryName(Convert.ToInt32(briefRow["F_CATEGORY_TYPE"])), briefRow["F_USER_ID"]);
      if (this._objectID < 0L || this._categoryID <= 0 || this._objectGUID.Length <= 0)
        return;
      this.IsValid = true;
    }
    catch (Exception ex)
    {
      this.UniIdentifiler = string.Format(LocalizationHolder.rm.GetString("Kernel_880"), (object) EnumDescConverter.GetEnumDescription((Enum) (ActionType) Convert.ToInt32(briefRow["F_RIGHT_ID"])), (object) Consts.GetCategoryName(Convert.ToInt32(briefRow["F_CATEGORY_TYPE"])), briefRow["F_USER_ID"]);
      this.ErrorException = ex;
      this.IsValid = false;
    }
  }

  public long ImportNewSecurity(SecurityRecord sr)
  {
    this.session.StartTransaction();
    try
    {
      long num = this.ExecAddSequrity(this.session.DataManager, sr);
      this.session.Commit();
      return num;
    }
    catch (Exception ex)
    {
      this.ErrorException = new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_909"), (object) sr.RightType, (object) sr.CategoryID, (object) sr.CategoryType, (object) ex.Message));
      this.session.Rollback();
      return 0;
    }
  }

  private long ExecAddSequrity(IDbManager db, SecurityRecord sr)
  {
    long num = 0;
    db.ExecuteSpNonQuery("IMS_ADD_CATEGORY_ACCESS", db.Parameter("inCATEGORY_TYPE", (object) sr.CategoryType), db.Parameter("inCATEGORY_ID", (object) sr.CategoryID), db.Parameter("inRIGHT_ID", (object) sr.RightId), db.Parameter("inUSER_ID", sr.UserId), db.Parameter("inRIGHT_TYPE", (object) sr.RightType), db.Parameter("inOWNER_ID", sr.OwnerId), db.Parameter("inPARENT_KEY", (object) 0L), db.OutputParameter("outKEY", (object) num));
    long int64 = Convert.ToInt64(db.GetOutputParameterValue("outKEY"));
    if (sr.EndDate is DateTime)
      db.ExecuteNonQuery("UPDATE IMS_CATEGORY_ACCESS SET F_BEGIN_DATE = :d0, F_END_DATE = :d1 WHERE F_KEY = :id", db.Parameter("d0", (object) Convert.ToDateTime(sr.BeginDate, (IFormatProvider) CultureInfo.InvariantCulture)), db.Parameter("d1", (object) Convert.ToDateTime(sr.EndDate, (IFormatProvider) CultureInfo.InvariantCulture)), db.Parameter("id", (object) int64));
    return int64;
  }

  public override bool Import()
  {
    this.session.StartTransaction();
    try
    {
      SecurityRecord sr = this.FormingSecurityRecord();
      IDbManager dataManager = this.session.DataManager;
      if (this._objectGUID != string.Empty && !this._importingSecObj.Contains((object) this._objectGUID))
      {
        dataManager.ExecuteNonQuery("DELETE FROM IMS_CATEGORY_ACCESS WHERE F_CATEGORY_TYPE = :aType AND F_CATEGORY_ID = :aID", dataManager.Parameter("aType", (object) this._categoryID), dataManager.Parameter("aID", (object) this._objectID));
        this._importingSecObj.Add((object) this._objectGUID);
      }
      sr.CategoryType = this._categoryID;
      sr.CategoryID = this._objectID;
      this.ExecAddSequrity(dataManager, sr);
      this.session.Commit();
      return true;
    }
    catch (Exception ex)
    {
      this.ErrorException = new Exception(this.UniIdentifiler, ex);
      this.session.Rollback();
      return false;
    }
  }

  private SecurityRecord FormingSecurityRecord()
  {
    SecurityRecord securityRecord = new SecurityRecord();
    foreach (DataColumn column in (InternalDataCollectionBase) this.briefRow.Table.Columns)
    {
      switch (column.ColumnName)
      {
        case "F_BEGIN_DATE":
          if (this.briefRow[column] != null && this.briefRow[column] != DBNull.Value)
          {
            securityRecord.BeginDate = (object) Convert.ToDateTime(this.briefRow[column], (IFormatProvider) CultureInfo.InvariantCulture);
            continue;
          }
          continue;
        case "F_END_DATE":
          if (this.briefRow[column] != null && this.briefRow[column] != DBNull.Value)
          {
            securityRecord.EndDate = (object) Convert.ToDateTime(this.briefRow[column], (IFormatProvider) CultureInfo.InvariantCulture);
            continue;
          }
          continue;
        case "F_OWNER_ID":
          if (GuidHelper.IsGuid(Convert.ToString(this.briefRow[column])))
          {
            IDBObject dbObject = this.session.GetObject(new Guid(Convert.ToString(this.briefRow[column])), false);
            if (dbObject != null)
            {
              securityRecord.OwnerId = (object) dbObject.ObjectID;
              continue;
            }
            continue;
          }
          securityRecord.OwnerId = (object) 0;
          continue;
        case "F_RIGHT_ID":
          securityRecord.RightId = Convert.ToInt32(this.briefRow[column]);
          continue;
        case "F_RIGHT_TYPE":
          securityRecord.RightType = Convert.ToInt32(this.briefRow[column]);
          continue;
        case "F_USER_ID":
          if (GuidHelper.IsGuid(Convert.ToString(this.briefRow[column])))
          {
            IDBObject dbObject = this.session.GetObject(new Guid(Convert.ToString(this.briefRow[column])), false);
            if (dbObject != null)
            {
              securityRecord.UserId = (object) dbObject.ObjectID;
              continue;
            }
            continue;
          }
          securityRecord.UserId = (object) 0;
          continue;
        default:
          continue;
      }
    }
    return securityRecord;
  }
}
