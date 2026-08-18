// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Services.RedliningService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Data;
using System.Diagnostics;


namespace Intermech.Kernel.Services;

public class RedliningService : LongLifeObject, IRedliningService
{
  private int levelID;
  private bool deleteFiles;
  private List<RedliningFiles> redliningFilesSettings = new List<RedliningFiles>();
  private object SyncRoot = new object();
  private long generation;
  private int _RedliningAttributeID;
  public readonly string REDLINING_SECTION = "RedliningSection";
  public readonly string LEVEL_ID_PARAM = nameof (LevelID);
  public readonly string DELETE_PARAM = "Delete";
  public readonly string GENERATION_PARAM = nameof (Generation);
  public readonly string REDLINING_SETTINGS = "RedliningSettings";

  public long Generation
  {
    [DebuggerStepThrough] get
    {
      lock (this.SyncRoot)
        return this.generation;
    }
  }

  public int LevelID
  {
    get
    {
      lock (this.SyncRoot)
        return this.levelID;
    }
  }

  public bool DeleteFiles
  {
    get
    {
      lock (this.SyncRoot)
        return this.deleteFiles;
    }
  }

  public List<RedliningFiles> RedliningFilesSettings
  {
    get
    {
      lock (this.SyncRoot)
        return this.redliningFilesSettings;
    }
  }

  private void NextGeneration(IUserSession session)
  {
    IDBConfigurations configurations = session.Configurations;
    lock (this.SyncRoot)
    {
      this.generation = configurations.ReadInteger("KERNEL", this.REDLINING_SECTION, this.GENERATION_PARAM, 0L, DBConfigMode.GlobalOnly);
      ++this.generation;
      configurations.WriteInteger("KERNEL", this.REDLINING_SECTION, this.GENERATION_PARAM, this.generation, 0L);
    }
  }

  public RedliningService(IUserSession session)
  {
    this._RedliningAttributeID = session.IdentHelper.GetAttributeID("cad0036f-306c-11d8-b4e9-00304f19f545");
    IDBConfigurations configurations = session.Configurations;
    this.deleteFiles = configurations.ReadBool("KERNEL", this.REDLINING_SECTION, this.DELETE_PARAM, false, DBConfigMode.GlobalOnly);
    this.levelID = this.deleteFiles ? (int) configurations.ReadInteger("KERNEL", this.REDLINING_SECTION, this.LEVEL_ID_PARAM, 0L, DBConfigMode.GlobalOnly) : 0;
    foreach (DataRow row in (InternalDataCollectionBase) configurations.ReadSection("KERNEL", this.REDLINING_SETTINGS, 0L).Rows)
      this.redliningFilesSettings.Add(new RedliningFiles(Convert.ToString(row[1])));
    this.generation = configurations.ReadInteger("KERNEL", this.REDLINING_SECTION, this.GENERATION_PARAM, 0L, DBConfigMode.GlobalOnly);
    if (this.generation != 0L)
      return;
    this.NextGeneration(session);
  }

  public int RedliningAttributeID => this._RedliningAttributeID;

  public bool IsRedliningFile(string mainFilePath, string verifiableFilePath)
  {
    lock (this.SyncRoot)
    {
      foreach (RedliningFiles redliningFilesSetting in this.redliningFilesSettings)
      {
        if (redliningFilesSetting.CheckRedliningFile(mainFilePath, verifiableFilePath))
          return true;
      }
    }
    return false;
  }

  public void ChangeRedliningSettings(
    List<RedliningFiles> settings,
    bool delete,
    int levelID,
    object sessionID)
  {
    IUserSession session = sessionID is IUserSession ? sessionID as IUserSession : UserSession.GetSessionByID((Guid) sessionID);
    IDBConfigurations configurations = session.Configurations;
    lock (this.SyncRoot)
    {
      configurations.WriteBool("KERNEL", this.REDLINING_SECTION, this.DELETE_PARAM, delete, 0L);
      configurations.WriteInteger("KERNEL", this.REDLINING_SECTION, this.LEVEL_ID_PARAM, (long) levelID, 0L);
      if (settings != null)
      {
        DataTable table = new DataTable();
        table.Columns.Add("F_PARAM_NAME", typeof (string));
        table.Columns.Add("F_VALUE", typeof (string));
        for (int index = 0; index < settings.Count; ++index)
        {
          RedliningFiles setting = settings[index];
          DataRow row = table.NewRow();
          row["F_PARAM_NAME"] = (object) index.ToString();
          row["F_VALUE"] = (object) setting.ToString();
          table.Rows.Add(row);
        }
        table.AcceptChanges();
        configurations.WriteSection("KERNEL", this.REDLINING_SETTINGS, table, 0L);
        this.redliningFilesSettings = settings;
      }
      this.deleteFiles = delete;
      this.levelID = levelID;
    }
    this.NextGeneration(session);
  }
}
