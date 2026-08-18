// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.ImportPumpRelation
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
using System.Text;


namespace Intermech.Kernel.Briefcase;

internal sealed class ImportPumpRelation : ImportRelation
{
  public ImportPumpRelation(
    UserSession Session,
    ImportingRelation BriefRelation,
    bool createLinksArray)
    : base(Session, BriefRelation, createLinksArray, true, false)
  {
    this.hintAppendEnable = true;
    long result;
    if (!long.TryParse(Convert.ToString(BriefRelation.Relation.ProjId), out result) || !long.TryParse(Convert.ToString(BriefRelation.Relation.PartId), out result))
      this.IsValid = false;
    if (!this.IsValid)
      return;
    this.uniIdentifiler = $"Связь тип={BriefRelation.Relation.RelationType}, Guid={{{BriefRelation.Relation.PrjLinkGuid}}}, ProjId({BriefRelation.Relation.ProjId})=>PartId({BriefRelation.Relation.PartId})";
  }

  public long AddNewRelation()
  {
    bool flag1 = false;
    long num;
    try
    {
      long creatorId = this.BriefRelation.Relation.CreatorID;
      if (this.createLinksArray && this.BriefRelation.Relation.CreatorID > 0L)
        this.BriefRelation.Relation.CreatorID = 0L;
      if (this.BriefRelation.Relation.PrjLinkId != 0L && this.BriefRelation.Relation.PrjLinkId != -1L)
      {
        num = this.BriefRelation.Relation.PrjLinkId;
        flag1 = true;
      }
      else
      {
        num = this.ExecAddImportedRelation((Guid) this.BriefRelation.Relation.PrjLinkGuid);
        this.BriefRelation.Relation.PrjLinkId = num;
      }
      ArrayList arrayList = new ArrayList();
      long dataValue1 = 0;
      long dataValue2 = 0;
      bool flag2 = this.BriefRelation.Relation.RelationType == MetaDataHelper.GetRelationTypeID("cad0036b-306c-11d8-b4e9-00304f19f545");
      int attributeTypeId1 = MetaDataHelper.GetAttributeTypeID("cad001c2-306c-11d8-b4e9-00304f19f545");
      int attributeTypeId2 = MetaDataHelper.GetAttributeTypeID("cad014d2-306c-11d8-b4e9-00304f19f545");
      if (this.createLinksArray && creatorId != 0L)
        this.ObjectLinks.Add((object) new RelationPropertiesLinks(num, creatorId, this.BriefRelation.Relation.RelationType));
      for (int index = 0; index < this.BriefRelation.Attributes.Count; ++index)
      {
        AttributeRecord attribute = this.BriefRelation.Attributes[index];
        IDBAttributeType attributeType4 = this.GetAttributeType4(attribute.AttributeId, this.BriefRelation.Relation.RelationType);
        if (flag2)
        {
          if (attribute.AttributeId == attributeTypeId1)
            dataValue1 = attribute.IntegerValue != null ? Convert.ToInt64(attribute.IntegerValue) : 0L;
          else if (attribute.AttributeId == attributeTypeId2)
          {
            dataValue2 = attribute.IntegerValue != null ? Convert.ToInt64(attribute.IntegerValue) : 0L;
            continue;
          }
        }
        try
        {
          if (attributeType4.AttributeType == FieldTypes.ftBlob || attributeType4.AttributeType == FieldTypes.ftFile || attributeType4.AttributeType == FieldTypes.ftMemo || attributeType4.AttributeType == FieldTypes.ftShortBlob)
          {
            attribute.IntegerValue = (object) 0L;
            ImportBlob importBlob = new ImportBlob(this.session, this.hintAppendEnable);
            attribute.IntegerValue = (object) importBlob.Import(num, attribute, attributeType4.AttributeType, true);
            if (attributeType4.AttributeID == this.session.IdentHelper.FileAttributeID)
              DBHelper.AddBatchSQL((IUserSession) this.session, (this.hintAppendEnable ? 1 : 0) != 0, "INSERT INTO IMS_FILENAMES (F_FILENAME, F_KEY, F_ID) VALUES (:fname, :objID, :id1)", new DbCommandParam[3]
              {
                this.session.DataManager.BatchParameter("fname", DbType.String, (object) attribute.StringValue.ToString().Trim().ToUpper()),
                this.session.DataManager.BatchParameter("objID", DbType.Int64, (object) num),
                this.session.DataManager.BatchParameter("id1", DbType.Int64, (object) num)
              });
          }
          if ((attributeType4.AttributeType == FieldTypes.ftObjectLink || attributeType4.AttributeType == FieldTypes.ftObjectLinkByID) && this.createLinksArray)
          {
            this.ObjectLinks.Add((object) new RelationLinks(num, attributeType4.AttributeID, attribute.InlistId, Convert.ToInt64(attribute.IntegerValue), Convert.ToString(attribute.StringValue), this.BriefRelation.Relation.RelationType, attributeType4.AttributeType == FieldTypes.ftObjectLinkByID));
            attribute.IntegerValue = (object) null;
            attribute.StringValue = (object) null;
          }
          if (attribute.IsNew)
            this.InsertAttribute(attributeType4, attribute, num);
          else
            this.UpdateAttribute(attributeType4, attribute, num);
          if (attribute.InlistId == 0)
          {
            string[] updateTables = this.session.DBCache.GetUpdateTables(attributeType4.AttributeID, -1, this.BriefRelation.Relation.RelationType);
            if (updateTables != null)
            {
              if (updateTables.Length != 0)
                arrayList.Add((object) this.AddViewFieldsToSQL(updateTables, attribute));
            }
          }
        }
        catch (Exception ex)
        {
          this.AddIntoLog(string.Format(LocalizationHolder.rm.GetString("Kernel_314"), (object) attribute.AttributeId, (object) num, (object) ex.Message));
        }
      }
      string[] updateTables1 = this.session.DBCache.GetUpdateTables(-1, -1, this.BriefRelation.Relation.RelationType);
      if (updateTables1 != null)
      {
        if (!flag1)
        {
          string format = "INSERT INTO {0} (F_PRJLINK_ID, F_PROJ_ID, F_PART_ID, F_RELATION_TYPE, F_CREATE_DATE, F_PRJ_GUID{1}) VALUES (:v_relID, :v_proj_id, :v_part_id, :v_reltype_id, :v_create, :v_prj_guid{2})";
          DbCommandParam[] collection = new DbCommandParam[6]
          {
            this.session.DataManager.BatchParameter("v_relID", DbType.Int64, (object) num),
            this.session.DataManager.BatchParameter("v_proj_id", DbType.Int64, this.BriefRelation.Relation.ProjId),
            this.session.DataManager.BatchParameter("v_part_id", DbType.Int64, this.BriefRelation.Relation.PartId),
            this.session.DataManager.BatchParameter("v_reltype_id", DbType.Int32, (object) this.BriefRelation.Relation.RelationType),
            this.session.DataManager.BatchParameter("v_create", DbType.DateTime, this.BriefRelation.Relation.CreateDate ?? (object) (DateTime.UtcNow + this.session.TimeZoneOffset)),
            this.session.DataManager.BatchParameter("v_prj_guid", DbType.Guid, (object) (Guid) this.BriefRelation.Relation.PrjLinkGuid)
          };
          foreach (string str in updateTables1)
          {
            try
            {
              List<DbCommandParam> dbCommandParamList = new List<DbCommandParam>((IEnumerable<DbCommandParam>) collection);
              string empty = string.Empty;
              string commandText;
              if (str.ToUpper() == "IMS_RELATIONS_VIEW")
              {
                commandText = string.Format(format, (object) str, (object) string.Empty, (object) string.Empty);
              }
              else
              {
                StringBuilder stringBuilder1 = new StringBuilder();
                StringBuilder stringBuilder2 = new StringBuilder();
                foreach (Importer.UpdatingAttribute updatingAttribute in arrayList)
                {
                  if (updatingAttribute.Tables.Contains(str))
                  {
                    foreach (Tuple<string, DbType, object> fieldsAndValue in updatingAttribute.FieldsAndValues)
                    {
                      stringBuilder1.Append(',');
                      stringBuilder1.Append(fieldsAndValue.Item1);
                      stringBuilder2.Append(", :");
                      stringBuilder2.Append(fieldsAndValue.Item1);
                      dbCommandParamList.Add(this.session.DataManager.BatchParameter(fieldsAndValue.Item1, fieldsAndValue.Item2, fieldsAndValue.Item3));
                    }
                  }
                }
                commandText = string.Format(format, (object) str, (object) stringBuilder1, (object) stringBuilder2);
              }
              DBHelper.AddBatchSQL((IUserSession) this.session, this.hintAppendEnable, commandText, dbCommandParamList.ToArray());
            }
            catch (Exception ex)
            {
              this.AddIntoLog(string.Format(LocalizationHolder.rm.GetString("Kernel_962"), (object) str, (object) num, (object) ex.Message));
            }
          }
        }
        else if (arrayList.Count > 0)
        {
          string format = "UPDATE {0} SET{1} WHERE F_PRJLINK_ID = :v_prjlinkID";
          foreach (string str in updateTables1)
          {
            try
            {
              List<DbCommandParam> dbCommandParamList = new List<DbCommandParam>();
              string commandText = string.Empty;
              if (str.ToUpper() != "IMS_RELATIONS_VIEW")
              {
                StringBuilder stringBuilder = new StringBuilder();
                foreach (Importer.UpdatingAttribute updatingAttribute in arrayList)
                {
                  if (updatingAttribute.Tables.Contains(str))
                  {
                    foreach (Tuple<string, DbType, object> fieldsAndValue in updatingAttribute.FieldsAndValues)
                    {
                      stringBuilder.AppendFormat(", {0} = :{0}", (object) fieldsAndValue.Item1);
                      dbCommandParamList.Add(this.session.DataManager.BatchParameter(fieldsAndValue.Item1, fieldsAndValue.Item2, fieldsAndValue.Item3));
                    }
                  }
                }
                if (stringBuilder.Length > 0)
                {
                  stringBuilder.Remove(0, 1);
                  commandText = string.Format(format, (object) str, (object) stringBuilder);
                }
              }
              if (commandText != string.Empty)
              {
                dbCommandParamList.Add(this.session.DataManager.BatchParameter("v_prjlinkID", DbType.Int64, (object) num));
                this.session.DataManager.AddBatchSQL(commandText, dbCommandParamList.ToArray());
              }
            }
            catch (Exception ex)
            {
              this.AddIntoLog(string.Format(LocalizationHolder.rm.GetString("Kernel_962"), (object) str, (object) num, (object) ex.Message));
            }
          }
        }
      }
      if (flag2)
        DBHelper.AddBatchSQL((IUserSession) this.session, (this.hintAppendEnable ? 1 : 0) != 0, "INSERT INTO IMS_VERSIONS_CONTEXT (F_CONTEXT_ID, F_ID, F_OBJECT_ID, F_MODIFICATION_ID) VALUES (:v_proj_id, :v_part_id, :v_comp_version_id, :v_modif_id)", new DbCommandParam[4]
        {
          this.session.DataManager.BatchParameter("v_proj_id", DbType.Int64, this.BriefRelation.Relation.ProjId),
          this.session.DataManager.BatchParameter("v_part_id", DbType.Int64, this.BriefRelation.Relation.PartId),
          this.session.DataManager.BatchParameter("v_comp_version_id", DbType.Int64, (object) dataValue1),
          this.session.DataManager.BatchParameter("v_modif_id", DbType.Int64, (object) dataValue2)
        });
    }
    catch (Exception ex)
    {
      this.AddIntoLog(string.Format(LocalizationHolder.rm.GetString("Kernel_963"), this.BriefRelation.Relation.ProjId, this.BriefRelation.Relation.PartId, (object) this.BriefRelation.Relation.RelationType, (object) ex.Message));
      num = 0L;
    }
    return num;
  }
}
