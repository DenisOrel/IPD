// Decompiled with JetBrains decompiler
// Type: Intermech.Kernel.ChangingAnalyzerJob
// Assembly: Intermech.Kernel, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: CD05141F-BA24-423B-ACBF-7E9D2BA2BC31
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.Kernel.dll

using Intermech.Interfaces;
using System;
using System.Collections.Generic;
using System.Threading;


namespace Intermech.Kernel;

public class ChangingAnalyzerJob
{
  public ObjectChangingAction Action;
  public Guid Guid;
  public Guid SessionGuid;
  public Thread Thread;
  public Dictionary<Guid, IObjectsChangingAnalyzer> Analyzers;
  public ChangingObjects Items;
  private ChangingAnalyzerJobStatus status;

  public ChangingAnalyzerJobStatus Status
  {
    get
    {
      lock (this.status)
        return this.status.Clone() as ChangingAnalyzerJobStatus;
    }
  }

  public ChangingAnalyzerJob(
    ObjectChangingAction action,
    Guid sessionGuid,
    Dictionary<Guid, IObjectsChangingAnalyzer> analyzers,
    ChangingObjects items)
  {
    this.Action = action;
    this.Guid = Guid.NewGuid();
    this.SessionGuid = sessionGuid;
    this.Thread = (Thread) null;
    this.Items = items;
    lock (analyzers)
      this.Analyzers = new Dictionary<Guid, IObjectsChangingAnalyzer>((IDictionary<Guid, IObjectsChangingAnalyzer>) analyzers);
    this.status = new ChangingAnalyzerJobStatus();
  }

  public override bool Equals(object obj)
  {
    return !(obj is ChangingAnalyzerJob changingAnalyzerJob) ? base.Equals(obj) : this.Guid.Equals(changingAnalyzerJob.Guid);
  }

  public override int GetHashCode() => this.Guid.GetHashCode();

  public virtual void ThreadMethod()
  {
    lock (this.status)
    {
      if (this.status.Progress != ChangingAnalyzerJobProgress.NotStarted)
        return;
      this.status.Start();
    }
    int objects = 0;
    try
    {
      IUserSession session = (UserSession.GetSessionByID(this.SessionGuid) as UserSession).Clone(nameof (ChangingAnalyzerJob));
      try
      {
        lock (this.status)
        {
          List<ChangingObject> changingObjects = this.Items.ExtractChangingObjects();
          for (int index = 0; index < changingObjects.Count; ++index)
          {
            if (this.Thread == null)
            {
              this.status.Progress = ChangingAnalyzerJobProgress.Cancelled;
              break;
            }
            changingObjects[index].LoadDescription(session);
          }
        }
        if (this.Analyzers.Count > 0)
        {
          foreach (KeyValuePair<Guid, IObjectsChangingAnalyzer> analyzer in this.Analyzers)
          {
            objects += analyzer.Value.Analyze(session, this.Items);
            lock (this.status)
            {
              this.status.Objects = (long) objects;
              if (this.Thread == null)
                this.status.Progress = ChangingAnalyzerJobProgress.Cancelled;
              if (this.status.Progress == ChangingAnalyzerJobProgress.Cancelled)
              {
                this.status.Cancel();
                break;
              }
            }
          }
        }
        lock (this.status)
        {
          if (this.status.Progress == ChangingAnalyzerJobProgress.Cancelled)
            return;
          List<ChangingObject> changingObjects = this.Items.ExtractChangingObjects();
          for (int index = 0; index < changingObjects.Count; ++index)
          {
            if (this.Thread == null)
            {
              this.status.Progress = ChangingAnalyzerJobProgress.Cancelled;
              break;
            }
            changingObjects[index].LoadDescription(session);
          }
        }
      }
      finally
      {
        session.Logout(nameof (ChangingAnalyzerJob));
        lock (this.status)
        {
          if (this.status.Progress != ChangingAnalyzerJobProgress.Cancelled)
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

  public virtual void Start()
  {
    this.Thread = new Thread(new ThreadStart(this.ThreadMethod));
    this.Thread.IsBackground = true;
    this.Thread.Name = $"{EnumTypeHelper.GetCaption((Enum) this.Action)}AnalyzeJob.{this.Guid.ToString()}";
    this.Thread.Start();
  }

  public virtual void Stop() => this.Thread = (Thread) null;
}
