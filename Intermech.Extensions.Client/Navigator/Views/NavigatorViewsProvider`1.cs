// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.NavigatorViewsProvider`1
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;
using Intermech.Extensions;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Navigator.Views;

public class NavigatorViewsProvider<TItem> : NavigatorViewProvider
{
  [NotNull]
  [ItemNotNull]
  private readonly NavigatorViewDescriptorBase[] _views;

  public NavigatorViewsProvider([NotEmpty] OneOrMore<NavigatorViewDescriptorBase> views)
  {
    NavigatorViewDescriptorBase[] viewDescriptorBaseArray;
    if (!views.OneValue)
      viewDescriptorBaseArray = (NavigatorViewDescriptorBase[]) (views.Values as NavigatorViewDescriptor<TItem>[]) ?? views.Values.AsArray<NavigatorViewDescriptorBase>();
    else
      viewDescriptorBaseArray = new NavigatorViewDescriptorBase[1]
      {
        views.Value
      };
    this._views = viewDescriptorBaseArray;
  }

  protected virtual bool IsViewSupportedBySelectedItem([NotNull] TItem item) => true;

  [NotNull]
  public override ViewsInfo GetViews([CanBeNull] ISelectedItems items, [NotNull] IServiceProvider services)
  {
    if (items == null)
      return ViewsInfo.Empty;
    int count = items.Count;
    IReadOnlyCollection<TItem> result;
    if (count == 0 || !items.TryGetAll<TItem>(new Func<TItem, bool>(this.IsViewSupportedBySelectedItem), out result))
      return ViewsInfo.Empty;
    ViewsInfo viewsInfo = (ViewsInfo) null;
    foreach (NavigatorViewDescriptor<TItem> view in this._views)
    {
      if ((view.SupportMultipleSelection || count <= 1) && (view.Filter == null || view.Filter(services, result)))
      {
        view.CheckViewRegistered();
        if (viewsInfo == null)
          viewsInfo = new ViewsInfo();
        viewsInfo.Add(view.ViewType.Name, view.HelpTopicID != 0 ? new ViewInfo(view.TriggerPriority, view.HelpTopicID, view.ViewType) : new ViewInfo(view.TriggerPriority, view.ViewType));
      }
    }
    return viewsInfo ?? ViewsInfo.Empty;
  }
}
