// Decompiled with JetBrains decompiler
// Type: Intermech.PdmConfigurator.Server.PdmOptionsAnalyzerJob
// Assembly: Intermech.PdmConfigurator.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 80F94CD1-7E39-423C-8BC4-966315C23D3C
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.PdmConfigurator.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.PdmConfigurator;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Threading;

#nullable disable
namespace Intermech.PdmConfigurator.Server;

internal sealed class PdmOptionsAnalyzerJob
{
  public Guid Guid;
  public Guid SessionGuid;
  public Thread Thread;
  public Dictionary<Guid, IPdmOptionsAnalyzer> Analyzers;
  public PdmAnalyzedOptionObjects Items;
  private PdmOptionsAnalyzerJobStatus status;
  private IList<long> excludedObjects;
  private IList<long> excludedOptions;
  private PdmAnalyzerFlags options;

  public PdmOptionsAnalyzerJobStatus Status
  {
    get
    {
      lock (this.status)
        return this.status.Clone() as PdmOptionsAnalyzerJobStatus;
    }
  }

  public PdmAnalyzerFlags Options => this.options;

  public PdmOptionsAnalyzerJob(
    Guid sessionGuid,
    Dictionary<Guid, IPdmOptionsAnalyzer> analyzers,
    PdmAnalyzedOptionObjects items,
    PdmAnalyzerFlags options,
    IList<long> excludedObjects,
    IList<long> excludedOptions)
  {
    this.Guid = Guid.NewGuid();
    this.SessionGuid = sessionGuid;
    this.Thread = (Thread) null;
    this.Items = items;
    this.options = options;
    this.excludedObjects = excludedObjects;
    this.excludedOptions = excludedOptions;
    lock (analyzers)
      this.Analyzers = new Dictionary<Guid, IPdmOptionsAnalyzer>((IDictionary<Guid, IPdmOptionsAnalyzer>) analyzers);
    this.status = new PdmOptionsAnalyzerJobStatus();
  }

  public override bool Equals(object obj)
  {
    return !(obj is PdmOptionsAnalyzerJob optionsAnalyzerJob) ? base.Equals(obj) : this.Guid.Equals(optionsAnalyzerJob.Guid);
  }

  public override int GetHashCode() => this.Guid.GetHashCode();

  internal void ThreadMethod()
  {
    lock (this.status)
    {
      if (this.status.Progress != PdmOptionsAnalyzerJobProgress.NotStarted)
        return;
      this.status.Start();
    }
    int objects1 = 0;
    try
    {
      IUserSession session = (UserSession.GetSessionByID(this.SessionGuid) as UserSession).Clone("PdmConfigurator.Thread");
      try
      {
        lock (this.status)
        {
          this.Items.CheckObjects(this.excludedOptions);
          List<PdmAnalyzedOptionObject> objects2 = this.Items.ExtractObjects();
          for (int index = 0; index < objects2.Count; ++index)
          {
            if (this.Thread == null)
            {
              this.status.Progress = PdmOptionsAnalyzerJobProgress.Cancelled;
              break;
            }
            PdmAnalyzedOptionObject analyzedOptionObject = objects2[index];
            analyzedOptionObject.LoadDescription(session);
            analyzedOptionObject.CheckOptions(session, this.options, this.excludedOptions);
          }
        }
        if (this.Analyzers.Count > 0)
        {
          foreach (KeyValuePair<Guid, IPdmOptionsAnalyzer> analyzer in this.Analyzers)
          {
            objects1 += analyzer.Value.Analyze(session, this.Items, this.options, this.excludedObjects, this.excludedOptions);
            lock (this.status)
            {
              this.status.Objects = (long) objects1;
              if (this.Thread == null)
                this.status.Progress = PdmOptionsAnalyzerJobProgress.Cancelled;
              if (this.status.Progress == PdmOptionsAnalyzerJobProgress.Cancelled)
              {
                this.status.Cancel();
                break;
              }
            }
          }
        }
        lock (this.status)
        {
          if (this.status.Progress == PdmOptionsAnalyzerJobProgress.Cancelled)
            return;
          for (int index = this.Items.Count - 1; index >= 0; --index)
          {
            PdmAnalyzedOptionObject analyzedOptionObject = this.Items[index];
            analyzedOptionObject.CheckOptions(session, this.options, this.excludedOptions);
            if (analyzedOptionObject.Options == null || analyzedOptionObject.Options.Count == 0 || this.excludedObjects != null && this.excludedObjects.IndexOf(analyzedOptionObject.ObjectID) >= 0)
            {
              this.Items.RemoveAt(index);
              --objects1;
            }
          }
        }
      }
      finally
      {
        session.Logout("PdmConfigurator.Thread");
        lock (this.status)
        {
          if (this.status.Progress != PdmOptionsAnalyzerJobProgress.Cancelled)
            this.status.Complete(objects1, this.Items);
        }
      }
    }
    catch (Exception ex)
    {
      lock (this.status)
        this.status.Error(ex, this.Items);
    }
  }

  internal void Start()
  {
    this.Thread = new Thread(new ThreadStart(this.ThreadMethod));
    this.Thread.IsBackground = true;
    this.Thread.Name = "PdmOptionsAnalyzerJob." + this.Guid.ToString();
    this.Thread.Start();
  }

  internal void Stop() => this.Thread = (Thread) null;
}
