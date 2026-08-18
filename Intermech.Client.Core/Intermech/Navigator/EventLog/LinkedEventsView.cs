
// Type: Intermech.Navigator.EventLog.LinkedEventsView
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Kernel.Search;
using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;
using System.Collections.Specialized;
using System.ComponentModel.Design;


namespace Intermech.Navigator.EventLog;

/// <summary>
/// Реализует закладку навигатора, предназначенную для промотра событий из журнала системы.
/// </summary>
[ViewDescriptionProvider(typeof (LinkedEventsView.LinkedEventsViewDescriptionProvider))]
public class LinkedEventsView : EventsView
{
  /// <summary>Список условий запроса</summary>
  public virtual ConditionStructure[] Conditions => (ConditionStructure[]) null;

  /// <summary>Дополнительные параметры запроса</summary>
  public virtual HybridDictionary ConditionTags => (HybridDictionary) null;

  protected override INode GetNode()
  {
    return (INode) new EventsNode(this.Conditions, this.ConditionTags)
    {
      Services = (IServiceProvider) this.Services
    };
  }

  protected override IServiceContainer GetServiceContainer()
  {
    IServiceContainer serviceContainer = base.GetServiceContainer();
    serviceContainer.AddService(typeof (IViewState), (object) new ViewStateService(ViewStateFlags.ReadOnly));
    return serviceContainer;
  }

  protected class LinkedEventsViewDescriptionProvider : EventsView.EventsViewDescriptionProvider
  {
    public override ViewDescription DoGetViewDescription(
      ISelectedItems selectedItems,
      IServiceProvider serviceProvider)
    {
      return base.DoGetViewDescription(selectedItems, serviceProvider);
    }
  }
}
