// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Briefcase.CheckApplicabilityCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Briefcase;
using Intermech.Localization;
using System;
using System.Data;


namespace Intermech.Kernel.Briefcase;

internal sealed class CheckApplicabilityCollection : CheckCollection
{
  private int _objTypeID;
  private DataSet _metaData;

  public CheckApplicabilityCollection(
    UserSession userSession,
    DataSet metaData,
    int objTypeID,
    string uniIdentifiler,
    CheckOptions options)
    : base(userSession, BriefcaseConsts.logApplicabilityCategory, options)
  {
    this._objTypeID = objTypeID;
    this._metaData = metaData;
    this.UniIdentifiler = string.Format(BriefcaseConsts.logAttribute4objTypeAddUniIdentifiler, (object) uniIdentifiler);
  }

  public override void Compare()
  {
    foreach (DataRow dataRow in this.session.DBCache.GetTable("IMS_TYPES_APPLICABILITY").Select(string.Format("{0}={1} OR {2}={1}", (object) "F_INOBJECT_TYPE", (object) this._objTypeID, (object) "F_OBJECT_TYPE")))
    {
      IDBRelationType relationType = this.session.GetRelationType(Convert.ToInt32(dataRow["F_RELATION_TYPE"]));
      Guid guid = (relationType as IDBGuid).GUID;
      string str1 = $"{{{guid.ToString()}}}";
      DataTable table1 = this._metaData.Tables["IMS_RELATION_TYPES"];
      guid = (relationType as IDBGuid).GUID;
      string filterExpression1 = $"{"F_GUID"} = {DataSetProcessor.QString(guid.ToString())}";
      DataRow[] dataRowArray1 = table1.Select(filterExpression1);
      if (dataRowArray1.Length == 0)
      {
        this.AddWarningToLog(BriefcaseConsts.logRelationTypeNotFoundInBriefcase, string.Empty, Helper.ValueToLog((object) relationType.Description, (object) (relationType as IDBGuid).GUID, true));
      }
      else
      {
        if (Convert.ToString(dataRowArray1[0]["F_DESCRIPTION"]) == relationType.Description)
          str1 = $"\"{relationType.Description}\"";
        IDBObjectType objectType1 = this.session.GetObjectType(Convert.ToInt32(dataRow["F_INOBJECT_TYPE"]));
        guid = (objectType1 as IDBGuid).GUID;
        string str2 = $"{{{guid.ToString()}}}";
        DataTable table2 = this._metaData.Tables["IMS_OBJECT_TYPES"];
        guid = (objectType1 as IDBGuid).GUID;
        string filterExpression2 = $"{"F_GUID"} = {DataSetProcessor.QString(guid.ToString())}";
        DataRow[] dataRowArray2 = table2.Select(filterExpression2);
        if (dataRowArray2.Length == 0)
        {
          this.AddWarningToLog(BriefcaseConsts.logObjectTypeNotFoundInBriefcase, string.Empty, Helper.ValueToLog((object) objectType1.ObjectTypeName, (object) (objectType1 as IDBGuid).GUID, true));
        }
        else
        {
          if (Convert.ToString(dataRowArray2[0]["F_OBJ_TYPE_NAME"]) == objectType1.ObjectTypeName)
            str2 = $"\"{objectType1.ObjectTypeName}\"";
          IDBObjectType objectType2 = this.session.GetObjectType(Convert.ToInt32(dataRow["F_OBJECT_TYPE"]));
          guid = (objectType2 as IDBGuid).GUID;
          string str3 = $"{{{guid.ToString()}}}";
          DataTable table3 = this._metaData.Tables["IMS_OBJECT_TYPES"];
          guid = (objectType2 as IDBGuid).GUID;
          string filterExpression3 = $"{"F_GUID"} = {DataSetProcessor.QString(guid.ToString())}";
          DataRow[] dataRowArray3 = table3.Select(filterExpression3);
          if (dataRowArray3.Length == 0)
          {
            this.AddWarningToLog(BriefcaseConsts.logObjectTypeNotFoundInBriefcase, string.Empty, Helper.ValueToLog((object) objectType2.ObjectTypeName, (object) (objectType2 as IDBGuid).GUID, true));
          }
          else
          {
            if (Convert.ToString(dataRowArray3[0]["F_OBJ_TYPE_NAME"]) == objectType2.ObjectTypeName)
              str3 = $"\"{objectType2.ObjectTypeName}\"";
            if (this._metaData.Tables["IMS_TYPES_APPLICABILITY"].Select($"{"F_RELATION_TYPE"}={dataRowArray1[0]["F_RELATION_TYPE"]} AND {"F_OBJECT_TYPE"}={dataRowArray3[0]["F_OBJECT_TYPE"]} AND {"F_INOBJECT_TYPE"}={dataRowArray2[0]["F_OBJECT_TYPE"]}").Length == 0)
              this.AddWarningToLog(BriefcaseConsts.logApplicabilityNotFoundInBriefcase, string.Empty, $"{LocalizationHolder.rm.GetString("Kernel_258")}{str1}: {str3}->{str2}");
          }
        }
      }
    }
  }
}
