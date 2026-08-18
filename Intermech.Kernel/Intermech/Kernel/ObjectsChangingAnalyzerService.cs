// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.ObjectsChangingAnalyzerService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel;

public class ObjectsChangingAnalyzerService : LongLifeObject, IObjectsChangingAnalyzerService
{
  [NonSerialized]
  public Dictionary<ObjectChangingAction, Dictionary<Guid, IObjectsChangingAnalyzer>> analyzers;
  [NonSerialized]
  public Dictionary<ObjectChangingAction, Dictionary<Guid, ChangingAnalyzerJob>> jobs;

  public ObjectsChangingAnalyzerService()
  {
    this.analyzers = new Dictionary<ObjectChangingAction, Dictionary<Guid, IObjectsChangingAnalyzer>>();
    this.analyzers.Add(ObjectChangingAction.CancelChanges, new Dictionary<Guid, IObjectsChangingAnalyzer>());
    this.analyzers.Add(ObjectChangingAction.CheckIn, new Dictionary<Guid, IObjectsChangingAnalyzer>());
    this.analyzers.Add(ObjectChangingAction.CheckOut, new Dictionary<Guid, IObjectsChangingAnalyzer>());
    this.analyzers.Add(ObjectChangingAction.SaveChanges, new Dictionary<Guid, IObjectsChangingAnalyzer>());
    this.jobs = new Dictionary<ObjectChangingAction, Dictionary<Guid, ChangingAnalyzerJob>>();
    this.jobs.Add(ObjectChangingAction.CancelChanges, new Dictionary<Guid, ChangingAnalyzerJob>());
    this.jobs.Add(ObjectChangingAction.CheckIn, new Dictionary<Guid, ChangingAnalyzerJob>());
    this.jobs.Add(ObjectChangingAction.CheckOut, new Dictionary<Guid, ChangingAnalyzerJob>());
    this.jobs.Add(ObjectChangingAction.SaveChanges, new Dictionary<Guid, ChangingAnalyzerJob>());
  }

  public virtual Guid Analyze(
    ObjectChangingAction action,
    Guid sessionGuid,
    ChangingObjects changingObjects)
  {
    if (changingObjects == null || changingObjects.Count == 0)
      return Guid.Empty;
    ChangingAnalyzerJob changingAnalyzerJob = (ChangingAnalyzerJob) null;
    switch (action)
    {
      case ObjectChangingAction.CancelChanges:
        changingAnalyzerJob = new ChangingAnalyzerJob(action, sessionGuid, this.analyzers[ObjectChangingAction.CancelChanges], changingObjects);
        break;
    }
    if (changingAnalyzerJob == null)
      return Guid.Empty;
    lock (this.jobs)
      this.jobs[action].Add(changingAnalyzerJob.Guid, changingAnalyzerJob);
    changingAnalyzerJob.Start();
    return changingAnalyzerJob.Guid;
  }

  public virtual ChangingAnalyzerJobStatus QueryJobStatus(Guid jobID)
  {
    lock (this.jobs)
    {
      foreach (ObjectChangingAction key in (ObjectChangingAction[]) Enum.GetValues(typeof (ObjectChangingAction)))
      {
        Dictionary<Guid, ChangingAnalyzerJob> job = this.jobs[key];
        if (job.ContainsKey(jobID))
        {
          ChangingAnalyzerJob changingAnalyzerJob = job[jobID];
          ChangingAnalyzerJobStatus status = changingAnalyzerJob.Status;
          if (status.Progress == ChangingAnalyzerJobProgress.NotStarted || status.Progress == ChangingAnalyzerJobProgress.Working)
            return status;
          job.Remove(jobID);
          changingAnalyzerJob.Stop();
          return status;
        }
      }
    }
    return (ChangingAnalyzerJobStatus) null;
  }

  public virtual bool CancelJob(Guid jobID)
  {
    lock (this.jobs)
    {
      foreach (ObjectChangingAction key in (ObjectChangingAction[]) Enum.GetValues(typeof (ObjectChangingAction)))
      {
        Dictionary<Guid, ChangingAnalyzerJob> job = this.jobs[key];
        if (job.ContainsKey(jobID))
        {
          ChangingAnalyzerJob changingAnalyzerJob = job[jobID];
          job.Remove(jobID);
          changingAnalyzerJob.Stop();
          return true;
        }
      }
    }
    return false;
  }

  public virtual bool RegisterAnalyzer(IObjectsChangingAnalyzer analyzer)
  {
    if (analyzer == null)
      return false;
    Dictionary<Guid, IObjectsChangingAnalyzer> analyzer1 = this.analyzers[analyzer.Action];
    if (analyzer1.ContainsValue(analyzer) || analyzer1.ContainsKey(analyzer.Guid))
      return false;
    analyzer1.Add(analyzer.Guid, analyzer);
    return true;
  }

  public virtual bool UnregisterAnalyzer(IObjectsChangingAnalyzer analyzer)
  {
    if (analyzer == null)
      return false;
    Dictionary<Guid, IObjectsChangingAnalyzer> analyzer1 = this.analyzers[analyzer.Action];
    if (!analyzer1.ContainsValue(analyzer) || !analyzer1.ContainsKey(analyzer.Guid))
      return false;
    analyzer1.Remove(analyzer.Guid);
    return true;
  }

  public virtual bool UnregisterAnalyzer(Guid analyzerGuid)
  {
    if (analyzerGuid == Guid.Empty)
      return false;
    foreach (ObjectChangingAction key in (ObjectChangingAction[]) Enum.GetValues(typeof (ObjectChangingAction)))
    {
      Dictionary<Guid, IObjectsChangingAnalyzer> analyzer = this.analyzers[key];
      if (analyzer.ContainsKey(analyzerGuid))
      {
        analyzer.Remove(analyzerGuid);
        return true;
      }
    }
    return false;
  }
}
