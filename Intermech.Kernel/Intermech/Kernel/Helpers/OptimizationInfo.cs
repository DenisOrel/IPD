// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.Helpers.OptimizationInfo
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Server;
using Intermech.Localization;
using System;
using System.Collections;
using System.Data;
using System.Threading;


namespace Intermech.Kernel.Helpers;

internal class OptimizationInfo
{
  public ArrayList Records = new ArrayList();
  private DateTime BeginOperationTime;
  public static Hashtable CachedRecords = Hashtable.Synchronized(new Hashtable());
  public static DateTime SaveCounterDate = DateTime.UtcNow + +TimeSpan.FromHours(1.0);

  public OptimizationInfo(IDbManager db)
  {
  }

  public void StartOperation() => this.BeginOperationTime = DateTime.Now;

  public void SaveToCache()
  {
    lock (OptimizationInfo.CachedRecords)
    {
      int milliseconds = (DateTime.Now - this.BeginOperationTime).Milliseconds;
      foreach (OptimizationValue record in this.Records)
      {
        Attribute4ID key = new Attribute4ID(record.AttributeID, record.ObjectTypeID, record.RelationTypeID);
        object cachedRecord = OptimizationInfo.CachedRecords[(object) key];
        if (cachedRecord == null)
        {
          OptimizationStatistics optimizationStatistics = new OptimizationStatistics();
          switch (record.Operation)
          {
            case RequestOperations.Read:
              optimizationStatistics.ReadCounter = 1;
              optimizationStatistics.ReadDuration = milliseconds;
              break;
            case RequestOperations.Seek:
              optimizationStatistics.SeekCounter = 1;
              optimizationStatistics.SeekDuration = milliseconds;
              break;
            case RequestOperations.Write:
              optimizationStatistics.WriteCounter = 1;
              optimizationStatistics.WriteDuration = milliseconds;
              break;
          }
          OptimizationInfo.CachedRecords[(object) key] = (object) optimizationStatistics;
        }
        else
        {
          OptimizationStatistics optimizationStatistics = (OptimizationStatistics) cachedRecord;
          switch (record.Operation)
          {
            case RequestOperations.Read:
              ++optimizationStatistics.ReadCounter;
              optimizationStatistics.ReadDuration += milliseconds;
              continue;
            case RequestOperations.Seek:
              ++optimizationStatistics.SeekCounter;
              optimizationStatistics.SeekDuration += milliseconds;
              continue;
            case RequestOperations.Write:
              ++optimizationStatistics.WriteCounter;
              optimizationStatistics.WriteDuration += milliseconds;
              continue;
            default:
              continue;
          }
        }
      }
      this.Records.Clear();
      if (!(OptimizationInfo.SaveCounterDate < DateTime.UtcNow))
        return;
      OptimizationInfo.SaveCounterDate = DateTime.UtcNow + TimeSpan.FromHours(1.0);
      new Thread(new ThreadStart(OptimizationInfo.InternalSaveToBase))
      {
        Name = "SaveOptimizationStat",
        IsBackground = true
      }.Start();
    }
  }

  internal static void InternalSaveToBase()
  {
    lock (OptimizationInfo.CachedRecords)
    {
      IDictionaryEnumerator enumerator = OptimizationInfo.CachedRecords.GetEnumerator();
      using (IDbManager dbManager = (ServerServices.GetService(typeof (IDbManagerService)) as IDbManagerService).CreateDbManager())
      {
        dbManager.BeginTransaction();
        try
        {
          while (enumerator.MoveNext())
          {
            Attribute4ID key = (Attribute4ID) enumerator.Key;
            IDbDataParameter dbDataParameter1 = dbManager.Parameter("aid1", (object) key.AttributeID);
            IDbDataParameter dbDataParameter2 = dbManager.Parameter("oid1", (object) key.ObjectTypeID);
            IDbDataParameter dbDataParameter3 = dbManager.Parameter("rid1", (object) key.RelationTypeID);
            OptimizationStatistics optimizationStatistics = (OptimizationStatistics) enumerator.Value;
            dbManager.ExecuteNonQuery("INSERT INTO IMS_OPTIMIZER_STAT (F_ATTRIBUTE_ID, F_OBJECT_TYPE, F_RELATION_TYPE, F_READ, F_SEEK, F_WRITE, F_READ_DURATION, F_SEEK_DURATION, F_WRITE_DURATION) VALUES (:aid1, :oid1, :rid1, :fread, :fseek, :fwrite, :fread_dr, :fseek_dr, :fwrite_dr)", dbDataParameter1, dbDataParameter2, dbDataParameter3, dbManager.Parameter("fread", (object) optimizationStatistics.ReadCounter), dbManager.Parameter("fseek", (object) optimizationStatistics.SeekCounter), dbManager.Parameter("fwrite", (object) optimizationStatistics.WriteCounter), dbManager.Parameter("fread_dr", (object) optimizationStatistics.ReadDuration), dbManager.Parameter("fseek_dr", (object) optimizationStatistics.SeekDuration), dbManager.Parameter("fwrite_dr", (object) optimizationStatistics.WriteDuration));
          }
          dbManager.Commit();
        }
        catch (Exception ex)
        {
          dbManager.Rollback();
          IEventLogHelper service1 = (IEventLogHelper) ServerServices.GetService(typeof (IEventLogHelper));
          IIDHelper service2 = ServerServices.GetService(typeof (IIDHelper)) as IIDHelper;
          string ObjectName = LocalizationHolder.rm.GetString("Kernel_368");
          string message = ex.Message;
          long sysdbaId = service2.SysdbaID;
          string machineName = EnvironmentConsts.MachineName;
          service1.AddEvent(0L, 0L, 14, 0L, ObjectName, message, ActionType.Save, EventlogRecordType.Error, sysdbaId, machineName, (IUserSession) null);
        }
        OptimizationInfo.CachedRecords.Clear();
      }
    }
  }
}
