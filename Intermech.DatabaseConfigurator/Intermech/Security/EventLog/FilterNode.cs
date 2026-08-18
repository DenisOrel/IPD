// Decompiled with JetBrains decompiler
// Type: Intermech.Security.EventLog.FilterNode
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Interfaces;
using Intermech.Interfaces.Client;
using Intermech.Kernel.Search;
using Intermech.Navigator.DB;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Security.EventLog;

public class FilterNode : 
  CompositeNode,
  IConditionsProvider,
  IFilterNode,
  INodeNotifications,
  IContextAware
{
  private IServiceProvider _services;
  private AdvancedServiceContainer _advancedServiceContainer = new AdvancedServiceContainer();
  private Guid filterGuid;
  private ConditionStructure[] condCache;

  void IFilterNode.Initialize(Guid filterGuid) => this.filterGuid = filterGuid;

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    Filter filter = FiltersManager.Filters.FindFilter(this.filterGuid);
    if (filter == null)
      return (List<PartSlot>) null;
    this.condCache = filter.QueryConditions;
    FilterNodePart part = new FilterNodePart((IConditionsProvider) this);
    part.Services = (IServiceProvider) this._advancedServiceContainer;
    return this.SlotsFromSinglePart((INodePart) part);
  }

  public override void Refresh()
  {
    Filter filter = FiltersManager.Filters.FindFilter(this.filterGuid);
    if (filter != null)
      this.condCache = filter.QueryConditions;
    else
      this.condCache = (ConditionStructure[]) null;
  }

  public ConditionStructure[] GetConditions() => this.condCache;

  public bool ConditionsChanged => false;

  public ProcessResult Process(NotificationEventArgs e, object additionalInfo)
  {
    if (!(e.EventName == "FilterChanged") || !(e is FilterEventArgs filterEventArgs) || !(filterEventArgs.FilterGuid == this.filterGuid))
      return ProcessResult.None;
    this.Refresh();
    return ProcessResult.RefreshNode;
  }

  public IServiceProvider Services
  {
    get => this._services;
    set
    {
      if (this._services == value)
        return;
      this._services = value;
      this._advancedServiceContainer.AdvancedProvider = this._services;
    }
  }
}
