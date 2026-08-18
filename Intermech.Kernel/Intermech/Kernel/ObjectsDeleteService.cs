// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.ObjectsDeleteService
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Kernel;

public class ObjectsDeleteService : LongLifeObject, IObjectsDeleteService
{
  [NonSerialized]
  public Dictionary<Guid, DeleteObjectsJob> jobs;

  public ObjectsDeleteService() => this.jobs = new Dictionary<Guid, DeleteObjectsJob>();

  public virtual Guid Delete(
    Guid sessionGuid,
    DeletingObjects deletingObjects,
    DeleteObjectsJobMode mode)
  {
    if (deletingObjects == null || deletingObjects.Count == 0)
      return Guid.Empty;
    DeleteObjectsJob deleteObjectsJob = new DeleteObjectsJob(sessionGuid, deletingObjects);
    lock (this.jobs)
      this.jobs.Add(deleteObjectsJob.Guid, deleteObjectsJob);
    deleteObjectsJob.Start(mode);
    return deleteObjectsJob.Guid;
  }

  public virtual DeleteObjectsJobStatus QueryJobStatus(Guid jobID)
  {
    lock (this.jobs)
    {
      if (!this.jobs.ContainsKey(jobID))
        return (DeleteObjectsJobStatus) null;
      DeleteObjectsJob job = this.jobs[jobID];
      DeleteObjectsJobStatus status = job.Status;
      if (status.Progress == DeleteObjectsJobProgress.NotStarted || status.Progress == DeleteObjectsJobProgress.Working || status.Progress == DeleteObjectsJobProgress.Idle)
        return status;
      this.jobs.Remove(jobID);
      job.Stop();
      return status;
    }
  }

  public virtual DeleteObjectsJobStatus CancelJob(Guid jobID)
  {
    lock (this.jobs)
    {
      if (!this.jobs.ContainsKey(jobID))
        return (DeleteObjectsJobStatus) null;
      DeleteObjectsJob job = this.jobs[jobID];
      job.Cancel();
      DeleteObjectsJobStatus status = job.Status;
      this.jobs.Remove(jobID);
      job.Stop();
      return status;
    }
  }

  public virtual DeleteObjectsJobStatus PauseJob(Guid jobID)
  {
    lock (this.jobs)
    {
      if (!this.jobs.ContainsKey(jobID))
        return (DeleteObjectsJobStatus) null;
      DeleteObjectsJob job = this.jobs[jobID];
      job.Pause();
      DeleteObjectsJobStatus status = job.Status;
      if (status.Progress == DeleteObjectsJobProgress.NotStarted || status.Progress == DeleteObjectsJobProgress.Working || status.Progress == DeleteObjectsJobProgress.Idle)
        return status;
      this.jobs.Remove(jobID);
      job.Stop();
      return status;
    }
  }

  public virtual DeleteObjectsJobStatus ResumeJob(Guid jobID, DeleteObjectsJobMode mode)
  {
    lock (this.jobs)
    {
      if (!this.jobs.ContainsKey(jobID))
        return (DeleteObjectsJobStatus) null;
      DeleteObjectsJob job = this.jobs[jobID];
      job.Resume(mode);
      DeleteObjectsJobStatus status = job.Status;
      if (status.Progress == DeleteObjectsJobProgress.NotStarted || status.Progress == DeleteObjectsJobProgress.Working || status.Progress == DeleteObjectsJobProgress.Idle)
        return status;
      this.jobs.Remove(jobID);
      job.Stop();
      return status;
    }
  }
}
