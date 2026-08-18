// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Snapshots.DBSnapshotRelationCollection
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces.Server;
using Intermech.Interfaces.Snapshots;
using System.Data;


namespace Intermech.Kernel.Snapshots;

public class DBSnapshotRelationCollection : DBSessionable, IDBSnapshotRelationCollection
{
  private long _SnapshotID;

  public DBSnapshotRelationCollection(UserSession session, long snapshotID)
    : base(session)
  {
    this._SnapshotID = snapshotID;
  }

  public DataTable Select(int relationTypeID)
  {
    IDbManager dataManager = this.UserSession.DataManager;
    DataTable dataTable;
    if (relationTypeID < 0)
      dataTable = dataManager.ExecuteDataTable("SELECT * FROM IMS_REL_SNAPSHOT WHERE F_SNAPSHOT_ID = :snapID", dataManager.Parameter("snapID", (object) this._SnapshotID));
    else
      dataTable = dataManager.ExecuteDataTable("SELECT * FROM IMS_REL_SNAPSHOT WHERE F_SNAPSHOT_ID = :snapID AND F_RELATION_TYPE = :relType", dataManager.Parameter("snapID", (object) this._SnapshotID), dataManager.Parameter("relType", (object) relationTypeID));
    return dataTable;
  }
}
