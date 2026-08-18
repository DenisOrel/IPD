// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.Client.CompositionCopying.Model.CopyingSession
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Integrators;
using Intermech.Tools.Integrators.CADInterface;
using System;
using System.Collections.Generic;
using System.Diagnostics;

#nullable disable
namespace Intermech.Tools.Client.CompositionCopying.Model;

internal sealed class CopyingSession
{
  private long uniqueId;
  private DBObjectGraph graph;
  private DeferredEventDispatcher deferredEventDispatcher;
  private VersionsRulePackage versionsRule;
  private ICopyingSessionServices services;
  private IIntegrator integrator;
  private CADHeuristics integratorHeuristics;
  private CADSettings cadSettings;
  private HashSet<DBObjectRecord> drawingWithoutAllModels;
  private List<UserWorkItem> userWorkItems;
  private List<string> rastrAndSubstrateExtensionsList;
  private CopyingSessionProcessingHistory processingHistory;

  public CopyingSession(
    long uniqueID,
    ICopyingSessionServices services,
    IIntegrator integrator,
    CADSettings cadSettings,
    CADHeuristics integratorHeuristics)
  {
    if (services == null)
      throw new ArgumentNullException(nameof (services));
    if (integrator == null)
      throw new ArgumentNullException(nameof (integrator));
    if (cadSettings == null)
      throw new ArgumentNullException(nameof (cadSettings));
    this.uniqueId = uniqueID;
    this.graph = new DBObjectGraph();
    this.deferredEventDispatcher = new DeferredEventDispatcher((IDeferredEventSource) new CopyingSessionDeferredEventSource(new LateBound<CopyingSession>((Func<CopyingSession>) (() => this))));
    this.deferredEventDispatcher.RegisterHandler<DBObjectReselectedDeferredEvent>((DeferredEventHandler<DBObjectReselectedDeferredEvent>) new CascadeUpdatesAfterReselectingDocuments());
    this.deferredEventDispatcher.RegisterHandler<DBObjectAttributesChangedDeferredEvent>((DeferredEventHandler<DBObjectAttributesChangedDeferredEvent>) new CascadeUpdatesAfterEditingAttributes());
    this.versionsRule = VersionsRuleSources.GetEditorRule();
    this.services = services;
    this.integrator = integrator;
    this.integratorHeuristics = integratorHeuristics;
    this.cadSettings = cadSettings;
    this.drawingWithoutAllModels = new HashSet<DBObjectRecord>();
    this.userWorkItems = new List<UserWorkItem>(0);
    this.rastrAndSubstrateExtensionsList = new List<string>()
    {
      ".jpg",
      ".bmp",
      ".gif",
      ".png",
      ".xls",
      ".xlsx",
      ".xlsm",
      ".pdf"
    };
    this.processingHistory = new CopyingSessionProcessingHistory();
  }

  public long UniqueId
  {
    [DebuggerStepThrough] get => this.uniqueId;
  }

  public DBObjectGraph Graph
  {
    [DebuggerStepThrough] get => this.graph;
  }

  public DeferredEventDispatcher DeferredEventDispatcher
  {
    [DebuggerStepThrough] get => this.deferredEventDispatcher;
  }

  public VersionsRulePackage VersionsRule
  {
    [DebuggerStepThrough] get => this.versionsRule;
    [DebuggerStepThrough] set
    {
      this.versionsRule = value != null ? value : throw new ArgumentNullException(nameof (value));
    }
  }

  public ICopyingSessionServices Services
  {
    [DebuggerStepThrough] get => this.services;
  }

  public IIntegrator Integrator
  {
    [DebuggerStepThrough] get => this.integrator;
  }

  public CADHeuristics IntegratorHeuristics
  {
    [DebuggerStepThrough] get => this.integratorHeuristics;
  }

  public CADSettings IntegratorSettings
  {
    [DebuggerStepThrough] get => this.cadSettings;
  }

  public HashSet<DBObjectRecord> DrawingWithoutAllModels
  {
    [DebuggerStepThrough] get => this.drawingWithoutAllModels;
  }

  public List<UserWorkItem> UserWorkItems
  {
    [DebuggerStepThrough] get => this.userWorkItems;
  }

  public List<string> RastrAndSubstrateExtensionsList
  {
    [DebuggerStepThrough] get => this.rastrAndSubstrateExtensionsList;
  }

  public CopyingSessionProcessingHistory ProcessingHistory
  {
    [DebuggerStepThrough] get => this.processingHistory;
  }
}
