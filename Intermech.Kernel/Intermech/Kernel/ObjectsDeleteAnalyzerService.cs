// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.ObjectsDeleteAnalyzerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel;

public class ObjectsDeleteAnalyzerService : LongLifeObject, IObjectsDeleteAnalyzerService
{
  [NonSerialized]
  public Dictionary<Guid, IObjectsDeleteAnalyzer> analyzers;
  [NonSerialized]
  public Dictionary<Guid, DeleteAnalyzerJob> jobs;

  public ObjectsDeleteAnalyzerService()
  {
    this.analyzers = new Dictionary<Guid, IObjectsDeleteAnalyzer>();
    this.jobs = new Dictionary<Guid, DeleteAnalyzerJob>();
  }

  public virtual DeletingObjects LoadDescriptions(Guid sessionGuid, DeletingObjects deletingObjects)
  {
    if (deletingObjects == null || deletingObjects.Count == 0)
      return deletingObjects;
    DeletingObjects deletingObjects1 = new DeletingObjects();
    try
    {
      IUserSession session = (UserSession.GetSessionByID(sessionGuid) as UserSession).Clone("DeleteAnalyzerService.LoadDescriptions");
      try
      {
        List<DeletingObject> deletingObjects2 = deletingObjects.ExtractDeletingObjects();
        for (int index = 0; index < deletingObjects2.Count; ++index)
        {
          DeletingObject deletingObject = deletingObjects2[index];
          deletingObject.LoadDescription(session);
          deletingObjects1.Add(deletingObject);
        }
      }
      finally
      {
        session.Logout("DeleteAnalyzerService.LoadDescriptions");
      }
    }
    catch
    {
    }
    return deletingObjects1;
  }

  public Guid Analyze(
    Guid sessionGuid,
    DeletingObjects deletingObjects,
    DeleteAnalyzerOptions options)
  {
    if (deletingObjects == null || deletingObjects.Count == 0)
      return Guid.Empty;
    DeleteAnalyzerJob deleteAnalyzerJob = new DeleteAnalyzerJob(sessionGuid, this.analyzers, deletingObjects, options);
    lock (this.jobs)
      this.jobs.Add(deleteAnalyzerJob.Guid, deleteAnalyzerJob);
    deleteAnalyzerJob.Start();
    return deleteAnalyzerJob.Guid;
  }

  public virtual DeleteAnalyzerJobStatus QueryJobStatus(Guid jobID)
  {
    lock (this.jobs)
    {
      if (!this.jobs.ContainsKey(jobID))
        return (DeleteAnalyzerJobStatus) null;
      DeleteAnalyzerJob job = this.jobs[jobID];
      DeleteAnalyzerJobStatus status = job.Status;
      if (status.Progress == DeleteAnalyzerJobProgress.NotStarted || status.Progress == DeleteAnalyzerJobProgress.Working)
        return status;
      this.jobs.Remove(jobID);
      job.Stop();
      return status;
    }
  }

  public virtual bool CancelJob(Guid jobID)
  {
    lock (this.jobs)
    {
      if (!this.jobs.ContainsKey(jobID))
        return false;
      DeleteAnalyzerJob job = this.jobs[jobID];
      this.jobs.Remove(jobID);
      job.Stop();
      return true;
    }
  }

  public virtual bool RegisterAnalyzer(IObjectsDeleteAnalyzer analyzer)
  {
    if (analyzer == null || this.analyzers.ContainsValue(analyzer) || this.analyzers.ContainsKey(analyzer.Guid))
      return false;
    this.analyzers.Add(analyzer.Guid, analyzer);
    return true;
  }

  public virtual bool UnregisterAnalyzer(IObjectsDeleteAnalyzer analyzer)
  {
    if (analyzer == null || !this.analyzers.ContainsValue(analyzer) || !this.analyzers.ContainsKey(analyzer.Guid))
      return false;
    this.analyzers.Remove(analyzer.Guid);
    return true;
  }

  public virtual bool UnregisterAnalyzer(Guid analyzerGuid)
  {
    if (analyzerGuid == Guid.Empty || !this.analyzers.ContainsKey(analyzerGuid))
      return false;
    this.analyzers.Remove(analyzerGuid);
    return true;
  }
}
