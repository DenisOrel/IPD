
// Type: Intermech.Navigator.EventLog.EventsNode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces;
using Intermech.Kernel.Search;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Parts;
using System;
using System.Collections.Generic;
using System.Collections.Specialized;


namespace Intermech.Navigator.EventLog;

public class EventsNode : CompositeNode, IContextAware
{
  /// <summary>Список условий запроса</summary>
  private ConditionStructure[] _conditions;
  /// <summary>Дополнительные параметры запроса</summary>
  private HybridDictionary _conditionTags;
  private IServiceProvider _services;
  private AdvancedServiceContainer _advancedServiceContainer = new AdvancedServiceContainer();

  public EventsNode(ConditionStructure[] conditions, HybridDictionary conditionTags)
  {
    this._conditions = conditions;
    this._conditionTags = conditionTags;
  }

  protected override List<PartSlot> CreateNonFolderSlots()
  {
    EventsNodePart part = new EventsNodePart(this._conditions, this._conditionTags);
    part.Services = (IServiceProvider) this._advancedServiceContainer;
    return this.SlotsFromSinglePart((INodePart) part);
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
