// Decompiled with JetBrains decompiler
// Type: Intermech.PdmConfigurator.Server.PdmCompositionBrowserJob
// Assembly: Intermech.PdmConfigurator.Server, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 80F94CD1-7E39-423C-8BC4-966315C23D3C
// Assembly location: D:\IPS\IPS.Installer.Full\InstServer\Server\Intermech.PdmConfigurator.Server.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Compositions;
using Intermech.Interfaces.PdmConfigurator;
using Intermech.Kernel;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Data;
using System.Threading;

#nullable disable
namespace Intermech.PdmConfigurator.Server;

internal sealed class PdmCompositionBrowserJob
{
  public Guid Guid;
  public Guid SessionGuid;
  public Thread Thread;
  public Dictionary<Guid, IPdmCompositionBrowser> Analyzers;
  public CompositionObjects Items;
  public RelationPair RootObject;
  public RelationPath RootObjectPath;
  public TraceLog Trace;
  public PdmCompositionBrowserEventArgs Args;
  private PdmCompositionBrowserJobStatus status;

  public PdmCompositionBrowserJobStatus Status
  {
    get
    {
      lock (this.status)
        return this.status.Clone() as PdmCompositionBrowserJobStatus;
    }
  }

  public PdmCompositionBrowserJob(
    Guid sessionGuid,
    Dictionary<Guid, IPdmCompositionBrowser> analyzers,
    RelationPair rootObject,
    RelationPath rootObjectPath,
    CompositionObjects items,
    PdmCompositionBrowserEventArgs args)
  {
    this.Guid = Guid.NewGuid();
    this.SessionGuid = sessionGuid;
    this.Thread = (Thread) null;
    this.RootObject = rootObject;
    this.RootObjectPath = rootObjectPath;
    this.Items = items;
    this.Args = args;
    this.Trace = (TraceLog) null;
    lock (analyzers)
      this.Analyzers = new Dictionary<Guid, IPdmCompositionBrowser>((IDictionary<Guid, IPdmCompositionBrowser>) analyzers);
    this.status = new PdmCompositionBrowserJobStatus();
  }

  public override bool Equals(object obj)
  {
    return !(obj is PdmCompositionBrowserJob compositionBrowserJob) ? base.Equals(obj) : this.Guid.Equals(compositionBrowserJob.Guid);
  }

  public override int GetHashCode() => this.Guid.GetHashCode();

  internal void ThreadMethod()
  {
    lock (this.status)
    {
      if (this.status.Progress != PdmCompositionBrowserJobProgress.NotStarted)
        return;
      this.status.Start();
    }
    this.Trace = new TraceLog();
    this.status.Trace = this.Trace;
    this.Args.Status = this.status;
    try
    {
      IUserSession session = (UserSession.GetSessionByID(this.SessionGuid) as UserSession).Clone("PDMCompositionBrowser.ThreadMethod");
      try
      {
        if (this.Args.FullTrace)
        {
          List<int> intList1 = new List<int>();
          List<int> intList2 = new List<int>();
          this.Args.Tags = this.Args.Tags ?? new HybridDictionary();
          this.Trace.Tags = this.Trace.Tags ?? new HybridDictionary();
          this.Args.Tags[(object) TraceLog.ObjectsWithRoutesGuid] = (object) new Dictionary<long, RelationPath>();
          this.Trace.Tags[(object) TraceLog.ObjectsWithRoutesGuid] = this.Args.Tags[(object) TraceLog.ObjectsWithRoutesGuid];
          this.Args.Tags[(object) TraceLog.RouteApplsGuid] = (object) intList1;
          this.Trace.Tags[(object) TraceLog.RouteApplsGuid] = (object) intList1;
          this.Args.Tags[(object) TraceLog.RouteDisabledApplsGuid] = (object) intList2;
          this.Trace.Tags[(object) TraceLog.RouteDisabledApplsGuid] = (object) intList2;
          DataTable applicabilitiesList = session.GetRelationsApplicabilityCollection().GetApplicabilitiesList(MetaDataHelper.GetRelationTypeID("cad0019f-306c-11d8-b4e9-00304f19f545"), MetaDataHelper.GetObjectTypeID("cad0016f-306c-11d8-b4e9-00304f19f545"), -1);
          if (applicabilitiesList != null && applicabilitiesList.Rows.Count > 0)
          {
            for (int index = 0; index < applicabilitiesList.Rows.Count; ++index)
            {
              IMSApplicability imsApplicability = new IMSApplicability();
              imsApplicability.Load(applicabilitiesList.Rows[index]);
              int inObjectType = imsApplicability.InObjectType;
              if (inObjectType != -1)
              {
                if (imsApplicability.ApplicabilityMode == ApplicabilityModes.Disabled)
                {
                  if (intList2.IndexOf(imsApplicability.InObjectType) < 0)
                    intList2.Add(imsApplicability.InObjectType);
                }
                else
                  intList1.Add(inObjectType);
              }
            }
          }
        }
        if (this.Analyzers.Count <= 0)
          return;
        foreach (KeyValuePair<Guid, IPdmCompositionBrowser> analyzer in this.Analyzers)
        {
          this.Trace.Merge(analyzer.Value.Browse(session, this.RootObject, this.RootObjectPath, this.Items, this.Args));
          lock (this.status)
          {
            this.status.Trace = this.Trace;
            if (this.Thread == null)
              this.status.Progress = PdmCompositionBrowserJobProgress.Cancelled;
            if (this.status.Progress == PdmCompositionBrowserJobProgress.Cancelled)
            {
              this.status.Cancel();
              break;
            }
          }
        }
      }
      finally
      {
        session.Logout("PDMCompositionBrowser.ThreadMethod");
        lock (this.status)
        {
          if (this.status.Progress != PdmCompositionBrowserJobProgress.Cancelled)
            this.status.Complete(this.Trace);
        }
      }
    }
    catch (Exception ex)
    {
      lock (this.status)
        this.status.Error(ex, this.Trace);
    }
  }

  internal void Start()
  {
    this.Thread = new Thread(new ThreadStart(this.ThreadMethod));
    this.Thread.IsBackground = true;
    this.Thread.Name = "PdmCompositionBrowserJob." + this.Guid.ToString();
    this.Thread.Start();
  }

  internal void Stop()
  {
    lock (this.Thread)
    {
      if (this.Thread != null)
        this.Thread.Abort();
      this.Thread = (Thread) null;
    }
  }
}
