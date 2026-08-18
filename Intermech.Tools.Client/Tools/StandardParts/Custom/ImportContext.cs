// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.StandardParts.Custom.ImportContext
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.IO;
using Intermech.Tools.Integrators;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.StandardParts.Custom;

internal sealed class ImportContext
{
  private VersionsRulePackage versionsRule;
  private IIntegrator integrator;
  private string rootPath;
  private bool clearDesignations;
  private bool fillNames;
  private bool fillEmptyNamesOnly;
  private bool linkToImbase;
  private bool correctPartTypes;
  private LocalId<int> stdModelType;
  private string[] partModelExts;
  private NotificationQueue notifyQueue;
  private List<string> protocol;
  private readonly PathCollection importHistory;

  public ImportContext()
  {
    this.notifyQueue = new NotificationQueue();
    this.protocol = new List<string>(1024 /*0x0400*/);
    this.importHistory = new PathCollection(1024 /*0x0400*/);
  }

  public VersionsRulePackage VersionsRule
  {
    get => this.versionsRule;
    set => this.versionsRule = value;
  }

  public IIntegrator Integrator
  {
    get => this.integrator;
    set => this.integrator = value;
  }

  public string RootPath
  {
    get => this.rootPath;
    set => this.rootPath = value;
  }

  public bool ClearDesignation
  {
    get => this.clearDesignations;
    set => this.clearDesignations = value;
  }

  public bool FillNames
  {
    get => this.fillNames;
    set => this.fillNames = value;
  }

  public bool FillEmptyNamesOnly
  {
    get => this.fillEmptyNamesOnly;
    set => this.fillEmptyNamesOnly = value;
  }

  public bool LinkToImbase
  {
    get => this.linkToImbase;
    set => this.linkToImbase = value;
  }

  public bool CorrectPartTypes
  {
    get => this.correctPartTypes;
    set => this.correctPartTypes = value;
  }

  public LocalId<int> StandardModelType
  {
    get => this.stdModelType;
    set => this.stdModelType = value;
  }

  public string[] PartModelExtensions
  {
    get => this.partModelExts;
    set => this.partModelExts = value;
  }

  public NotificationQueue NotifyQueue
  {
    get => this.notifyQueue;
    set => this.notifyQueue = value;
  }

  public List<string> Protocol => this.protocol;

  public PathCollection ImportHistory => this.importHistory;
}
