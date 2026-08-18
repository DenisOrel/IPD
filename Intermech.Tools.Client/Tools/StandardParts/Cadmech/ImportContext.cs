// Decompiled with JetBrains decompiler
// Type: Intermech.Tools.StandardParts.Cadmech.ImportContext
// Assembly: Intermech.Tools.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: ED7849C5-DE41-4371-894D-DD4E15C9E1D9
// Assembly location: D:\IPS\Client\Intermech.Tools.Client.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Tools.Integrators;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Tools.StandardParts.Cadmech;

internal sealed class ImportContext
{
  private VersionsRulePackage versionsRule;
  private IIntegrator integrator;
  private LocalId<int> stdModelType;
  private List<LocalId<int>> asmModelTypes;
  private List<LocalId<int>> partModelTypes;
  private string[] partModelExts;
  private NotificationQueue notifyQueue;

  public ImportContext()
  {
    this.asmModelTypes = new List<LocalId<int>>(32 /*0x20*/);
    this.partModelTypes = new List<LocalId<int>>(32 /*0x20*/);
    this.notifyQueue = new NotificationQueue();
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

  public LocalId<int> StandardModelType
  {
    get => this.stdModelType;
    set => this.stdModelType = value;
  }

  public List<LocalId<int>> AssemblyModelTypes => this.asmModelTypes;

  public List<LocalId<int>> PartModelTypes => this.partModelTypes;

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
}
