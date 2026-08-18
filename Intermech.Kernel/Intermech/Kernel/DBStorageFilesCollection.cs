// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DBStorageFilesCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Kernel.Search;
using Intermech.Localization;
using System;
using System.Data;
using System.Diagnostics;


namespace Intermech.Kernel;

internal class DBStorageFilesCollection : DBRecordSet
{
  private IDbManager _DBManager;

  public DBStorageFilesCollection(
    UserSession uSession,
    int eventType,
    string storageName,
    IDbManager db)
    : base(uSession, eventType)
  {
    this._DBObjectTableName = storageName;
    this._DBKeyField = "F_FILE_ID";
    this._DBAttributesTableName = "IMS_OBJECT_ATTRS";
    this._DBKeyFieldID = Convert.ToInt32((object) ObligatoryObjectAttributes.F_FILE_ID);
    this.InitSecurityOptions(10, 0L);
    this._DBManager = db;
  }

  internal override IDbManager DBManager => this._DBManager;

  protected override AttributeSourceTypes AutoAttributeSourceTypes
  {
    [DebuggerStepThrough] get => AttributeSourceTypes.FileStorage;
  }

  protected override void InitSecurityOptions(int aCategoryType, long aCategoryID)
  {
    base.InitSecurityOptions(aCategoryType, aCategoryID);
    this.AccessActions.Add(ActionType.List, false);
  }

  public override string ObjectName
  {
    get => LocalizationHolder.rm.GetString("Kernel_365") + this._DBObjectTableName;
  }

  protected override IDBAttributeType[] GetColumnsCollection(
    ref DBRecordSetParams pars,
    bool failIfNotFound)
  {
    if (pars.Columns == null || pars.Columns.Length == 0)
      pars.Columns = new object[7]
      {
        (object) ObligatoryObjectAttributes.F_FILE_ID,
        (object) ObligatoryObjectAttributes.F_FILENAME,
        (object) ObligatoryObjectAttributes.F_FILESIZE,
        (object) ObligatoryObjectAttributes.F_FILEDATE,
        (object) ObligatoryObjectAttributes.F_ZIPSIZE,
        (object) ObligatoryObjectAttributes.F_NOTE,
        (object) ObligatoryObjectAttributes.F_OBJECTLINK_ID
      };
    return base.GetColumnsCollection(ref pars, failIfNotFound);
  }

  public override DataTable Select(DBRecordSetParams paramSet)
  {
    this.UserSession.QueryBuilder.OptimizedTypeID = -1;
    this.UserSession.QueryBuilder.SystemTableName = this._DBObjectTableName;
    return base.Select(paramSet);
  }

  protected override string GetWhereSQL(ConditionStructure[] conditions, int recordsCount)
  {
    string whereSql = base.GetWhereSQL(conditions, recordsCount);
    return !(whereSql.Trim() == string.Empty) ? $"{whereSql} AND (F_ATTRIBUTE_ID <> {-2000.ToString()})" : " WHERE F_ATTRIBUTE_ID <> " + -2000.ToString();
  }
}
