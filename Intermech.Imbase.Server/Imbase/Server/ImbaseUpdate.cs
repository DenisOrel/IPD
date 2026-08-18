// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.Server.ImbaseUpdate
// Assembly: Intermech.Imbase.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 5829B58F-0012-4316-BC33-53BA510970AF
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Imbase.Server.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Imbase;
using Intermech.Interfaces.MetadataUpdates;
using Intermech.Interfaces.Server;
using Intermech.Kernel;
using System;
using System.Data;

#nullable disable
namespace Intermech.Imbase.Server;

public class ImbaseUpdate : IUpdatable
{
  private int _version;
  private int _revision;

  public string[] GetUpdateScripts()
  {
    return new string[3]
    {
      "Intermech.Imbase.Attributes.xml",
      "Intermech.Imbase.ObjectsTypes.xml",
      "Intermech.Imbase.Objects.xml"
    };
  }

  public void BeforeExecScript(IUserSession session, string scriptName)
  {
  }

  public void AfterExecScript(IUserSession session, string scriptName)
  {
  }

  public void AfterExecAllScripts(IUserSession session) => this.PatchDataBase(session);

  private void PatchDataBase(IUserSession session)
  {
    if (!(session is UserSession userSession) || userSession.DataManager == null || !(userSession.DataManager.DataProvider.Name == "Sql"))
      return;
    this._version = 4;
    this._revision = 1;
    if (!this.IsNeedUpdate(userSession.DataManager, this._version, this._revision))
      return;
    userSession.DataManager.BeginTransaction();
    try
    {
      this.PatchDB_V4R1(userSession.DataManager);
      this.UpdateVersion(userSession.DataManager, this._version, this._revision);
      userSession.DataManager.Commit();
    }
    catch (Exception ex)
    {
      userSession.DataManager.Rollback();
    }
  }

  private bool IsNeedUpdate(IDbManager dbManager, int version, int revision)
  {
    bool flag = true;
    DataTable dataTable = dbManager.ExecuteDataTable("SELECT F_VERSION_ID, F_REVISION_ID FROM IMS_DBVERSION WHERE F_MODULE_NAME = 'IMBASE.INDEX'");
    if (dataTable != null && dataTable.Rows.Count > 0)
    {
      object obj1 = dataTable.Rows[0]["F_VERSION_ID"];
      int result1 = 0;
      if (int.TryParse(Convert.ToString(obj1), out result1))
      {
        if (result1 == version)
        {
          object obj2 = dataTable.Rows[0]["F_REVISION_ID"];
          int result2 = 0;
          if (int.TryParse(Convert.ToString(obj2), out result2))
            flag = result2 < revision;
        }
        else if (result1 > version)
          flag = false;
      }
    }
    return flag;
  }

  private void PatchDB_V4R1(IDbManager dbManager)
  {
    DataTable dataTable = dbManager.ExecuteDataTable("SELECT * FROM IMS_IMBASE_INDEXES");
    if (dataTable == null)
      return;
    dbManager.ExecuteNonQuery(sc_8122.ssp_appserver_8123());
    string format = "CREATE TABLE IMS_IMBASE_INDEXES ({0} BigNumber_DEF NOT NULL, {1} INTEGER NOT NULL, {2} INTEGER NOT NULL, {3} MaximumString_DEF NOT NULL, {4} INTEGER NOT NULL)";
    dbManager.ExecuteNonQuery(string.Format(format, (object) IndexesField.F_CATALOG_ID, (object) IndexesField.F_ATTRIBUTE_ID, (object) IndexesField.F_FLAG, (object) IndexesField.F_TABLE_NAME, (object) IndexesField.F_ATTRIBUTE_STATE));
    dbManager.ExecuteNonQuery($"ALTER TABLE IMS_IMBASE_INDEXES ADD PRIMARY KEY CLUSTERED ({IndexesField.F_CATALOG_ID}, {IndexesField.F_ATTRIBUTE_ID})");
    string commandText = $"INSERT INTO IMS_IMBASE_INDEXES ({IndexesField.F_CATALOG_ID}, {IndexesField.F_ATTRIBUTE_ID}, {IndexesField.F_FLAG}, {IndexesField.F_TABLE_NAME}, {IndexesField.F_ATTRIBUTE_STATE}) VALUES (:parCatalogID, :parAttributeID, :parFlag, :parTableName, :parState)";
    foreach (DataRow row in (InternalDataCollectionBase) dataTable.Rows)
      dbManager.ExecuteNonQuery(commandText, dbManager.Parameter(":parCatalogID", row[IndexesField.F_CATALOG_ID]), dbManager.Parameter(":parAttributeID", row[IndexesField.F_ATTRIBUTE_ID]), dbManager.Parameter(":parFlag", row[IndexesField.F_FLAG]), dbManager.Parameter(":parTableName", row[IndexesField.F_TABLE_NAME]), dbManager.Parameter(":parState", row[IndexesField.F_ATTRIBUTE_STATE]));
    dataTable.Dispose();
  }

  private void UpdateVersion(IDbManager dbManager, int version, int revision)
  {
    dbManager.ExecuteNonQuery("DELETE FROM IMS_DBVERSION WHERE F_MODULE_NAME = 'IMBASE.INDEX'");
    dbManager.ExecuteScalar($"INSERT INTO IMS_DBVERSION VALUES('IMBASE.INDEX',{version},{revision})");
  }
}
