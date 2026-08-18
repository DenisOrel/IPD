// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.DeleteAnalyzerJob
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;


namespace Intermech.Kernel;

public class DeleteAnalyzerJob
{
  public Guid Guid;
  public Guid SessionGuid;
  public Thread Thread;
  public Dictionary<Guid, IObjectsDeleteAnalyzer> Analyzers;
  public DeletingObjects Items;
  private DeleteAnalyzerJobStatus status;
  private DeleteAnalyzerOptions options;

  public DeleteAnalyzerJobStatus Status
  {
    get
    {
      lock (this.status)
        return this.status.Clone() as DeleteAnalyzerJobStatus;
    }
  }

  public DeleteAnalyzerOptions Options => this.options;

  public DeleteAnalyzerJob(
    Guid sessionGuid,
    Dictionary<Guid, IObjectsDeleteAnalyzer> analyzers,
    DeletingObjects items,
    DeleteAnalyzerOptions options)
  {
    this.Guid = Guid.NewGuid();
    this.SessionGuid = sessionGuid;
    this.Thread = (Thread) null;
    this.Items = items;
    this.options = options;
    lock (analyzers)
      this.Analyzers = new Dictionary<Guid, IObjectsDeleteAnalyzer>((IDictionary<Guid, IObjectsDeleteAnalyzer>) analyzers);
    this.status = new DeleteAnalyzerJobStatus();
  }

  public override bool Equals(object obj)
  {
    return !(obj is DeleteAnalyzerJob deleteAnalyzerJob) ? base.Equals(obj) : this.Guid.Equals(deleteAnalyzerJob.Guid);
  }

  public override int GetHashCode() => this.Guid.GetHashCode();

  protected virtual void ThreadMethod()
  {
    lock (this.status)
    {
      if (this.status.Progress != DeleteAnalyzerJobProgress.NotStarted)
        return;
      this.status.Start();
    }
    int objects = 0;
    try
    {
      IUserSession session = (UserSession.GetSessionByID(this.SessionGuid) as UserSession).Clone(nameof (DeleteAnalyzerJob));
      try
      {
        lock (this.status)
        {
          List<DeletingObject> deletingObjects = this.Items.ExtractDeletingObjects();
          for (int index = 0; index < deletingObjects.Count; ++index)
          {
            if (this.Thread == null)
            {
              this.status.Progress = DeleteAnalyzerJobProgress.Cancelled;
              break;
            }
            deletingObjects[index].LoadDescription(session);
          }
        }
        if (this.Analyzers.Count > 0)
        {
          foreach (KeyValuePair<Guid, IObjectsDeleteAnalyzer> analyzer in this.Analyzers)
          {
            objects += analyzer.Value.Analyze(session, this.Items, this.options);
            lock (this.status)
            {
              this.status.Objects = (long) objects;
              if (this.Thread == null)
                this.status.Progress = DeleteAnalyzerJobProgress.Cancelled;
              if (this.status.Progress == DeleteAnalyzerJobProgress.Cancelled)
              {
                this.status.Cancel();
                break;
              }
            }
          }
        }
        lock (this.status)
        {
          if (this.status.Progress == DeleteAnalyzerJobProgress.Cancelled)
            return;
          List<DeletingObject> deletingObjects = this.Items.ExtractDeletingObjects();
          for (int index = 0; index < deletingObjects.Count; ++index)
          {
            if (this.Thread == null)
            {
              this.status.Progress = DeleteAnalyzerJobProgress.Cancelled;
              break;
            }
            deletingObjects[index].LoadDescription(session);
          }
        }
      }
      finally
      {
        session.Logout(nameof (DeleteAnalyzerJob));
        lock (this.status)
        {
          if (this.status.Progress != DeleteAnalyzerJobProgress.Cancelled)
            this.status.Complete(objects, this.Items);
        }
      }
    }
    catch (Exception ex)
    {
      lock (this.status)
        this.status.Error(ex, this.Items);
    }
  }

  protected internal virtual void Start()
  {
    this.Thread = new Thread(new ThreadStart(this.ThreadMethod));
    this.Thread.IsBackground = true;
    this.Thread.Name = "DeleteAnalyzeJob." + this.Guid.ToString();
    this.Thread.Start();
  }

  protected internal virtual void Stop() => this.Thread = (Thread) null;
}
