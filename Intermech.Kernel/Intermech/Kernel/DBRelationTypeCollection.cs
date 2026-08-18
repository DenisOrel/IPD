// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBRelationTypeCollection
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
using System.Text;


namespace Intermech.Kernel;

internal class DBRelationTypeCollection : 
  DBCollection,
  IDBRelationTypeCollection,
  IDBCollection,
  IDBSecurity
{
  private static Dictionary<ActionType, bool> metadataActions = new Dictionary<ActionType, bool>(3);

  static DBRelationTypeCollection()
  {
    DBRelationTypeCollection.metadataActions.Add(ActionType.GetAccess, false);
    DBRelationTypeCollection.metadataActions.Add(ActionType.SetAccess, false);
    DBRelationTypeCollection.metadataActions.Add(ActionType.Create, false);
  }

  public DBRelationTypeCollection(UserSession uSession, bool filterRecs)
    : base(uSession, filterRecs)
  {
    this._DBTableName = "IMS_RELATION_TYPES";
    this._DBKeyField = "F_RELATION_TYPE";
    this._AreaSupport = filterRecs;
    this._LanguageSupport = false;
    this.InitSecurityOptions(6, 0L);
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    this.InitStaticSecurityOptions(aCategoryType, aCategoryID, DBRelationTypeCollection.metadataActions);
  }

  public override string ObjectName => LocalizationHolder.rm.GetString("Kernel_561");

  public DataTable GetUsedByAttribute(int attributeID)
  {
    StringBuilder stringBuilder = new StringBuilder();
    DataTable table = this.UserSession.DBCache.GetTable("IMS_ATTR4RELATION_TYPES");
    DataRow[] dataRowArray = table.Select("F_ATTRIBUTE_ID = " + attributeID.ToString());
    int columnIndex = table.Columns.IndexOf("F_RELATION_TYPE");
    if (dataRowArray.Length == 0)
    {
      stringBuilder.Append("-1");
    }
    else
    {
      stringBuilder.Append(dataRowArray[0][columnIndex].ToString());
      for (int index = 1; index < dataRowArray.Length; ++index)
        stringBuilder.AppendFormat(",{0}", dataRowArray[index][columnIndex]);
    }
    DataTable usedByAttribute = this.UserSession.DBCache.GetTable("IMS_RELATION_TYPES").Clone();
    DataRow[] fromRows = this.UserSession.DBCache.GetTable("IMS_RELATION_TYPES").Select($"F_RELATION_TYPE IN ({stringBuilder.ToString()})");
    SqlHelper.AssignRows(usedByAttribute, (IEnumerable<DataRow>) fromRows);
    this.DeleteNotVisibleRows(usedByAttribute);
    usedByAttribute.AcceptChanges();
    this.FillCaptions(usedByAttribute);
    return usedByAttribute;
  }

  public int Create(RelationTypeProperties relationProperties)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    this._LastEventID = this.AddEvent(0L, ActionType.Create, EventlogRecordType.AccessDenied, string.Format(LocalizationHolder.rm.GetString("Kernel_892"), (object) relationProperties.Description));
    this.CheckAccess(ActionType.Create);
    try
    {
      SqlHelper.ValidateEmptyValue(relationProperties.Description, LocalizationHolder.rm.GetString(sc_13667.ssp_appserver_13668()));
      SqlHelper.ValidateEmptyValue(relationProperties.TypeName, LocalizationHolder.rm.GetString("MDChildsName"));
      SqlHelper.ValidateEmptyValue(relationProperties.ReverseName, LocalizationHolder.rm.GetString(sc_13667.ssp_appserver_13669()));
      if (relationProperties.RelationTypeGuid == Guid.Empty)
        relationProperties.RelationTypeGuid = Guid.NewGuid();
      if (relationProperties.AreaID != "")
        this.UserSession.GetSubjectAreaCollection().ValidateAriasString(relationProperties.AreaID);
      if (relationProperties.ShortName != string.Empty && this.UserSession.DBCache.GetTable("IMS_RELATION_TYPES").Select("F_SHORT_NAME = " + SqlHelper.QString(relationProperties.ShortName)).Length != 0)
        throw new KernelExceptionID(sc_13667.ssp_appserver_13670(1646565617), (object) relationProperties.ShortName);
      dataManager.ExecuteSpNonQuery(sc_13667.ssp_appserver_13671(), dataManager.Parameter("inDESCRIPTION", (object) relationProperties.Description), dataManager.Parameter("inTYPE_NAME", (object) relationProperties.TypeName), dataManager.Parameter("inREVERSE_NAME", (object) relationProperties.ReverseName), dataManager.Parameter("inNOTE", (object) relationProperties.Note), dataManager.Parameter("inCHKOUTFILE", (object) Convert.ToInt32(relationProperties.CheckoutFile)), dataManager.Parameter("inRELATION_KIND", (object) Convert.ToInt32(0)), dataManager.Parameter("inSAVE_HISTORY", (object) Convert.ToInt32(relationProperties.SaveHistory)), dataManager.Parameter("inGUID", (object) relationProperties.RelationTypeGuid.ToString()), dataManager.Parameter("inAREA_ID", (object) relationProperties.AreaID), dataManager.Parameter("inANY_ATTRIBUTES", (object) Convert.ToInt32(relationProperties.AnyAttributes)), dataManager.Parameter("inSHORT_NAME", (object) relationProperties.ShortName), dataManager.OutputParameter("outRELATION_TYPE", (object) relationProperties.RelationType));
      relationProperties.RelationType = Convert.ToInt32(dataManager.GetOutputParameterValue("outRELATION_TYPE"));
      DataTable dataTable = dataManager.ExecuteDataTable("SELECT * FROM IMS_RELATION_TYPES WHERE F_RELATION_TYPE = " + relationProperties.RelationType.ToString());
      if (dataTable.Rows.Count != 1)
        throw new KernelException(string.Format(LocalizationHolder.rm.GetString(sc_13667.ssp_appserver_13672()), (object) relationProperties.RelationType));
      this.UserSession.DBCache.AddRow("IMS_RELATION_TYPES", dataTable.Rows[0], (IUserSession) this.UserSession);
      DBRelationType relationType = this.UserSession.GetRelationType(relationProperties.RelationType) as DBRelationType;
      relationType.Options = relationProperties.Options;
      relationType.SetCreatorAccess();
      (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).CloseEvent(this._LastEventID, 0L, (long) relationProperties.RelationType, string.Format(LocalizationHolder.rm.GetString("Kernel_894"), (object) relationProperties.Description), "", EventlogRecordType.AccessGranted, (IUserSession) this.UserSession);
      return relationProperties.RelationType;
    }
    catch (Exception ex)
    {
      string str = string.Format(LocalizationHolder.rm.GetString(sc_13667.ssp_appserver_13673()), (object) relationProperties.Description, (object) ex.Message);
      if (ex.Message.IndexOf(sc_13667.ssp_appserver_13674()) >= 0)
        str = string.Format(LocalizationHolder.rm.GetString(sc_13667.ssp_appserver_13675()), (object) relationProperties.Description);
      this.CloseEvent(this._LastEventID, EventlogRecordType.Error, str);
      if (!(ex is AccessDeniedException))
        throw new KernelException(str, ex);
      throw;
    }
  }
}
