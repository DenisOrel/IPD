// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.RestoreMetaDataExtentions
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Localization;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;


namespace Intermech.Kernel.Briefcase;

internal sealed class RestoreMetaDataExtentions(
  IUserSession session,
  List<IDСorresponds> importingObjectIDs,
  ImportEventLog eventLog) : RestoreImportingValues<DataSet[]>(session, importingObjectIDs, eventLog)
{
  protected override void OnRestore(DataSet[] item, BriefcaseImportProgress bip)
  {
    DataSet metadata = item[0];
    DataSet metadataImportList = item[1];
    DataTable table = metadata.Tables["IMS_MD_EXTENSIONS"];
    if (table.Rows.Count == 0)
      return;
    DataRow[] dataRowArray = metadataImportList.Tables[BriefcaseConsts.XmlMetadataTableName].Select($"{BriefcaseConsts.XmlCategoryTag}={(object) 3}");
    string format = "F_ATTRIBUTE_ID = {0} AND F_OBJECT_TYPE = -1 AND F_RELATION_TYPE = -1";
    for (int index1 = 0; index1 < dataRowArray.Length; ++index1)
    {
      int int32_1 = Convert.ToInt32(dataRowArray[index1][BriefcaseConsts.XmlIdTag]);
      DataRow[] source = table.Select(string.Format(format, (object) int32_1), "F_PARAM_NAME,F_INLIST_ID");
      if (source != null && source.Length != 0)
      {
        IDBAttributeType attributeType = this.session.GetAttributeType(new Guid(Convert.ToString(dataRowArray[index1][BriefcaseConsts.XmlExternalTag])), true);
        IDbDataParameter dbDataParameter1 = (this.session as UserSession).DataManager.Parameter("attrID", (object) attributeType.AttributeID);
        foreach (object obj in ((IEnumerable<DataRow>) source).Select<DataRow, object>((System.Func<DataRow, object>) (row => row["F_PARAM_NAME"])).Distinct<object>().ToList<object>())
          (this.session as UserSession).DataManager.ExecuteNonQuery("DELETE FROM IMS_MD_EXTENSIONS WHERE F_PARAM_NAME = :paramName AND " + string.Format(format, (object) ":attrID"), (this.session as UserSession).DataManager.Parameter("paramName", obj), dbDataParameter1);
        for (int index2 = 0; index2 < source.Length; ++index2)
        {
          int int32_2 = Convert.ToInt32(source[index2]["F_CATEGORY_TYPE"]);
          Convert.ToInt32(source[index2]["F_INLIST_ID"]);
          IDbDataParameter dbDataParameter2 = (this.session as UserSession).DataManager.Parameter("paramName", (object) Convert.ToString(source[index2]["F_PARAM_NAME"]));
          try
          {
            object extentionData = this.GetExtentionData(metadata, int32_2, source[index2]["F_VALUE"]);
            (this.session as UserSession).DataManager.ExecuteNonQuery("INSERT INTO IMS_MD_EXTENSIONS (F_ATTRIBUTE_ID,F_OBJECT_TYPE ,F_RELATION_TYPE,F_PARAM_NAME,F_INLIST_ID,F_CATEGORY_TYPE,F_VALUE) VALUES(:attrID, -1, -1, :paramName, :inlistID, :categoryID, :value)", dbDataParameter1, dbDataParameter2, (this.session as UserSession).DataManager.Parameter("inlistID", (object) Convert.ToInt32(source[index2]["F_INLIST_ID"])), (this.session as UserSession).DataManager.Parameter("categoryID", (object) int32_2), (this.session as UserSession).DataManager.Parameter("value", (object) Convert.ToString(extentionData)));
          }
          catch (Exception ex)
          {
            this.eventLog.AddToTrace(string.Format(LocalizationHolder.rm.GetString("Kernel_964"), (object) attributeType.Name, (object) ex.Message));
          }
        }
      }
    }
    this.ImportExMetadataCategory(table, metadata, metadataImportList, 4, "F_OBJECT_TYPE");
    this.ImportExMetadataCategory(table, metadata, metadataImportList, 6, "F_RELATION_TYPE");
  }

  private void ImportExMetadataCategory(
    DataTable extTable,
    DataSet metadata,
    DataSet metadataImportList,
    int category,
    string categoryField)
  {
    DataRow[] dataRowArray1 = metadataImportList.Tables[BriefcaseConsts.XmlMetadataTableName].Select($"{BriefcaseConsts.XmlCategoryTag}={(object) category}");
    for (int index1 = 0; index1 < dataRowArray1.Length; ++index1)
    {
      int int32_1 = Convert.ToInt32(dataRowArray1[index1][BriefcaseConsts.XmlIdTag]);
      DataRow[] dataRowArray2 = extTable.Select($"{categoryField} = {int32_1}", "F_ATTRIBUTE_ID,F_PARAM_NAME,F_INLIST_ID");
      if (dataRowArray2 != null && dataRowArray2.Length != 0)
      {
        IDBAttributableType attributableType = category != 6 ? (IDBAttributableType) this.session.GetObjectType(new Guid(Convert.ToString(dataRowArray1[index1][BriefcaseConsts.XmlExternalTag])), false) : (IDBAttributableType) this.session.GetRelationType(new Guid(Convert.ToString(dataRowArray1[index1][BriefcaseConsts.XmlExternalTag])), false);
        if (attributableType == null)
        {
          this.eventLog.AddToTrace(category == 6 ? string.Format(LocalizationHolder.rm.GetString("Kernel_965"), dataRowArray1[index1][BriefcaseConsts.XmlExternalTag]) : string.Format(LocalizationHolder.rm.GetString("Kernel_966"), dataRowArray1[index1][BriefcaseConsts.XmlExternalTag]));
        }
        else
        {
          string parameterName = category == 6 ? "relType" : "objType";
          Dictionary<int, int> dictionary = new Dictionary<int, int>(dataRowArray2.Length);
          IDbDataParameter dbDataParameter1 = (this.session as UserSession).DataManager.Parameter(parameterName, (object) (category == 6 ? ((IDBRelationType) attributableType).RelationType : ((IDBObjectType) attributableType).ObjectType));
          for (int index2 = 0; index2 < dataRowArray2.Length; ++index2)
          {
            int int32_2 = Convert.ToInt32(dataRowArray2[index2]["F_CATEGORY_TYPE"]);
            int int32_3 = Convert.ToInt32(dataRowArray2[index2]["F_ATTRIBUTE_ID"]);
            int num = 0;
            if (int32_3 != -1 && int32_3 != 0)
            {
              if (!dictionary.TryGetValue(int32_3, out num))
              {
                num = Helper.GetConformityAttribureType(this.session as UserSession, metadata.Tables["IMS_ATTRIBUTES"], int32_3);
                dictionary.Add(int32_3, num);
                if (num == 0)
                {
                  this.eventLog.AddToTrace(category == 6 ? string.Format(LocalizationHolder.rm.GetString("Kernel_1153"), (object) ((IDBRelationType) attributableType).Description, (object) int32_3) : string.Format(LocalizationHolder.rm.GetString("Kernel_967"), (object) ((IDBObjectType) attributableType).ObjectTypeName, (object) int32_3));
                  continue;
                }
              }
              else if (num == 0)
                continue;
            }
            int int32_4 = Convert.ToInt32(dataRowArray2[index2]["F_INLIST_ID"]);
            IDbDataParameter dbDataParameter2 = (this.session as UserSession).DataManager.Parameter("paramName", (object) Convert.ToString(dataRowArray2[index2]["F_PARAM_NAME"]));
            IDbDataParameter dbDataParameter3 = (this.session as UserSession).DataManager.Parameter("attrID", (object) num);
            if (int32_4 == 0)
              (this.session as UserSession).DataManager.ExecuteNonQuery($"DELETE FROM IMS_MD_EXTENSIONS WHERE F_PARAM_NAME = :paramName AND {categoryField} = :{parameterName} AND F_ATTRIBUTE_ID = :attrID", dbDataParameter2, dbDataParameter1, dbDataParameter3);
            try
            {
              object extentionData = this.GetExtentionData(metadata, int32_2, dataRowArray2[index2]["F_VALUE"]);
              (this.session as UserSession).DataManager.ExecuteNonQuery($"INSERT INTO IMS_MD_EXTENSIONS (F_ATTRIBUTE_ID,F_OBJECT_TYPE ,F_RELATION_TYPE,F_PARAM_NAME,F_INLIST_ID,F_CATEGORY_TYPE,F_VALUE) VALUES(:attrID, {(category == 6 ? (object) "-1" : (object) ":objType")}, {(category == 6 ? (object) ":relType" : (object) "-1")}, :paramName, :inlistID, :categoryID, :value)", dbDataParameter3, dbDataParameter1, dbDataParameter2, (this.session as UserSession).DataManager.Parameter("inlistID", (object) Convert.ToInt32(dataRowArray2[index2]["F_INLIST_ID"])), (this.session as UserSession).DataManager.Parameter("categoryID", (object) int32_2), (this.session as UserSession).DataManager.Parameter("value", (object) Convert.ToString(extentionData)));
            }
            catch (Exception ex)
            {
              this.eventLog.AddToTrace(category == 6 ? string.Format(LocalizationHolder.rm.GetString("Kernel_1154"), (object) ((IDBRelationType) attributableType).Description, (object) ex.Message) : string.Format(LocalizationHolder.rm.GetString("Kernel_968"), (object) ((IDBObjectType) attributableType).ObjectTypeName, (object) ex.Message));
            }
          }
        }
      }
    }
  }

  private object GetExtentionData(DataSet metadata, int categoryID, object value)
  {
    switch (categoryID)
    {
      case 1:
        long oldID = Convert.ToInt64(value);
        return (object) ((oldID != 0L ? this.importingObjectIDs.Find((Predicate<IDСorresponds>) (x => x.SourceObjectID == oldID)) : (IDСorresponds) null) ?? throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_976"), value))).HostObjectID;
      case 3:
        int conformityAttribureType = Helper.GetConformityAttribureType(this.session as UserSession, metadata.Tables["IMS_ATTRIBUTES"], Convert.ToInt32(value));
        return conformityAttribureType != 0 ? (object) conformityAttribureType : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_969"), value));
      case 4:
        int int32 = Convert.ToInt32(value);
        if (int32 == -1)
          return (object) int32;
        int conformityObjectType = Helper.GetConformityObjectType((IUserSession) (this.session as UserSession), metadata.Tables["IMS_OBJECT_TYPES"], int32);
        return conformityObjectType != -1 ? (object) conformityObjectType : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_975"), value));
      case 6:
        int conformityRelationType = Helper.GetConformityRelationType((IUserSession) (this.session as UserSession), metadata.Tables["IMS_RELATION_TYPES"], Convert.ToInt32(value));
        return conformityRelationType != -1 ? (object) conformityRelationType : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_977"), value));
      case 7:
        int conformityLcStep = Helper.GetConformityLCStep(this.session as UserSession, metadata.Tables["IMS_LC_STEPS"], Convert.ToInt32(value));
        return conformityLcStep != -1 ? (object) conformityLcStep : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_974"), value));
      case 8:
        int conformityLcLevel = Helper.GetConformityLCLevel(this.session as UserSession, metadata.Tables["IMS_LEVELS"], Convert.ToInt32(value));
        return conformityLcLevel != 0 ? (object) conformityLcLevel : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_972"), value));
      case 9:
        string conformityLanguage = Helper.GetConformityLanguage(this.session as UserSession, metadata, Convert.ToString(value));
        return !(conformityLanguage == string.Empty) ? (object) conformityLanguage : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_971"), value));
      case 11:
        string conformitySubjectAreas = Helper.GetConformitySubjectAreas((IUserSession) (this.session as UserSession), metadata, Convert.ToString(value));
        return !(conformitySubjectAreas == string.Empty) ? (object) conformitySubjectAreas : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_978"), value));
      case 12:
        return (object) (this.session.GetAttributesGroup(new Guid(metadata.Tables["IMS_ATTR_GROUPS"].Rows.Find((object) Convert.ToInt32(value))["F_GUID"].ToString()), false) ?? throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_970"), value))).GroupID;
      case 16 /*0x10*/:
        int conformityLcSchemes = Helper.GetConformityLCSchemes(this.session as UserSession, metadata, Convert.ToInt32(value));
        return conformityLcSchemes != -1 ? (object) conformityLcSchemes : throw new Exception(string.Format(LocalizationHolder.rm.GetString("Kernel_973"), value));
      default:
        return value;
    }
  }
}
