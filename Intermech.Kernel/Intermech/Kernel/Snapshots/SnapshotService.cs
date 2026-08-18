// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Snapshots.SnapshotService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using ImSSP;
using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Interfaces.Snapshots;
using System;
using System.Collections.Generic;
using System.Data;


namespace Intermech.Kernel.Snapshots;

public class SnapshotService : LongLifeObject, ISnapshotService
{
  private volatile int _MaxIterationsPerObject;
  private volatile int _IterationLifetime;
  private volatile int _TruncateLevel = -1;
  private const string IterationSectionName = "ITERATIONS";
  private const string MaxIterationsParamName = "MAX_ITERATIONS";
  private const string IterationLifetimeParamName = "LIFETIME";
  private const string TruncateLevelParamName = "TRUNCATE_LEVEL";

  public SnapshotService(IUserSession session)
  {
    this.Loadsettings(session);
    (ServerServices.GetService(typeof (IEventLogHelper)) as IEventLogHelper).AfterCacheReload += new CacheReloadHandler(this.AfterCacheReload);
  }

  private void Loadsettings(IUserSession session)
  {
    this._MaxIterationsPerObject = Convert.ToInt32(session.Configurations.ReadInteger("KERNEL", "ITERATIONS", "MAX_ITERATIONS", 0L, DBConfigMode.GlobalOnly));
    this._IterationLifetime = Convert.ToInt32(session.Configurations.ReadInteger("KERNEL", "ITERATIONS", "LIFETIME", 0L, DBConfigMode.GlobalOnly));
    string input = session.Configurations.ReadString("KERNEL", "ITERATIONS", "TRUNCATE_LEVEL", string.Empty, DBConfigMode.GlobalOnly);
    Guid result;
    if (input != string.Empty && Guid.TryParse(input, out result))
    {
      IDBLifecycleLevelType lifecycleLevel = session.GetLifecycleLevel(result, false);
      if (lifecycleLevel != null)
        this._TruncateLevel = lifecycleLevel.LevelID;
      else
        this._TruncateLevel = -1;
    }
    else
      this._TruncateLevel = -1;
  }

  private void AfterCacheReload(IDbManager db)
  {
    IUserSession sessionTemporaryClone = (ServerServices.GetService(typeof (IDBTimedEvents)) as IDBTimedEvents).GetSystemSessionTemporaryClone(nameof (AfterCacheReload));
    try
    {
      this.Loadsettings(sessionTemporaryClone);
    }
    finally
    {
      sessionTemporaryClone.Logout(nameof (AfterCacheReload));
    }
  }

  private int DeleteSnapshots(UserSession session, DataTable tbl, List<string> log)
  {
    int num = 0;
    for (int index = 0; index < tbl.Rows.Count; ++index)
    {
      try
      {
        IDBObjectSnapshot snapshot = session.GetSnapshot(Convert.ToInt64(tbl.Rows[index][0]), false);
        if (snapshot != null)
        {
          snapshot.Delete((long) Consts.PurgeMode);
          ++num;
        }
      }
      catch (Exception ex)
      {
        log.Add($"Ошибка удаления итерации номер {tbl.Rows[index][0]}: {ex.Message}");
        log.Add(ex.StackTrace);
        log.Add(string.Empty);
      }
    }
    return num;
  }

  internal int DeleteOldSnapshots(UserSession session, List<string> logList)
  {
    int num1 = 0;
    IDbManager dataManager = session.DataManager;
    if (this._TruncateLevel >= 0)
    {
      DataTable tbl = dataManager.ExecuteDataTable("SELECT S.F_SNAPSHOT_ID FROM IMS_SNAPSHOTS S where EXISTS(SELECT * FROM IMS_OBJECTS O WHERE O.F_OBJECT_ID = S.F_OBJECT_ID AND O.F_LEVEL_ID = :levelID)", dataManager.Parameter("levelID", (object) this._TruncateLevel));
      int num2 = this.DeleteSnapshots(session, tbl, logList);
      if (num2 > 0)
      {
        logList.Add($"Удалено итераций на уровне продвижения '{session.GetLifecycleLevel(this._TruncateLevel).LevelName}': {num2}");
        num1 += num2;
      }
    }
    if (this._IterationLifetime > 0)
    {
      DateTime dateTime = DateTime.UtcNow - TimeSpan.FromDays((double) this._IterationLifetime);
      DataTable tbl = dataManager.ExecuteDataTable("SELECT S.F_SNAPSHOT_ID FROM IMS_SNAPSHOTS S where F_SNAPSHOT_DATE < :trunc_date", dataManager.Parameter("trunc_date", (object) dateTime));
      int num3 = this.DeleteSnapshots(session, tbl, logList);
      if (num3 > 0)
      {
        logList.Add($"Удалено итераций старше {dateTime}: {num3}");
        num1 += num3;
      }
    }
    if (this._MaxIterationsPerObject > 0)
    {
      DataTable dataTable = dataManager.ExecuteDataTable("SELECT S1.F_SNAPSHOT_ID, S1.F_OBJECT_ID FROM IMS_SNAPSHOTS S1 WHERE (SELECT COUNT(F_OBJECT_ID) FROM IMS_SNAPSHOTS S2 WHERE S2.F_OBJECT_ID = S1.F_OBJECT_ID GROUP BY F_OBJECT_ID) > :max_iters ORDER BY S1.F_OBJECT_ID ASC, S1.F_SNAPSHOT_ID DESC", dataManager.Parameter("max_iters", (object) this._MaxIterationsPerObject));
      long num4 = 0;
      int num5 = 0;
      int num6 = 0;
      for (int index = 0; index < dataTable.Rows.Count; ++index)
      {
        long int64 = Convert.ToInt64(dataTable.Rows[index][1]);
        if (num4 != int64)
        {
          num5 = 1;
          num4 = int64;
        }
        else if (++num5 > this._MaxIterationsPerObject)
        {
          IDBObjectSnapshot snapshot = session.GetSnapshot(Convert.ToInt64(dataTable.Rows[index][0]), false);
          if (snapshot != null)
          {
            snapshot.Delete((long) Consts.PurgeMode);
            ++num6;
          }
        }
      }
      if (num6 > 0)
      {
        logList.Add($"Удалено итераций, вышедших за лемит на одну версию объекта: {num6}");
        num1 += num6;
      }
    }
    return num1;
  }

  public SnapshotSettings GetSnapshotSettings()
  {
    return new SnapshotSettings(this._MaxIterationsPerObject, this._IterationLifetime, this._TruncateLevel);
  }

  public void SetSnapshotSettings(Guid userSession, SnapshotSettings settings)
  {
    IUserSession sessionById = UserSession.GetSessionByID(userSession);
    if (!sessionById.IsAdmin)
      throw new KernelExceptionID(sc_14229.ssp_appserver_14230(2117254962));
    sessionById.Configurations.WriteInteger("KERNEL", "ITERATIONS", "MAX_ITERATIONS", (long) settings.MaxIterationsPerObject, 0L);
    this._MaxIterationsPerObject = settings.MaxIterationsPerObject;
    sessionById.Configurations.WriteInteger("KERNEL", "ITERATIONS", "LIFETIME", (long) settings.IterationLifetime, 0L);
    this._IterationLifetime = settings.IterationLifetime;
    string str = settings.TruncateLevel != -1 ? sessionById.GetLifecycleLevel(settings.TruncateLevel).GUID.ToString() : string.Empty;
    this._TruncateLevel = settings.TruncateLevel;
    sessionById.Configurations.WriteString("KERNEL", "ITERATIONS", "TRUNCATE_LEVEL", str, 0L);
  }
}
