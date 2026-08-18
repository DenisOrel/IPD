// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.Navigator
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.DataFormats;
using Intermech.Diagnostics;
using Intermech.Navigator.Views;
using System;
using System.Collections.Generic;
using System.Linq;

#nullable disable
namespace Intermech.Extensions;

public abstract class Navigator
{
  [NotNull]
  private static readonly object _registerObjectViewSync = new object();
  [NotNull]
  [ItemNotNull]
  private static readonly HashSet<NavigatorViewDescriptorBase> _viewsToCheckRegistered = new HashSet<NavigatorViewDescriptorBase>();
  private static bool _pluginsLoadCompleteHandlerSet;

  private static void RegisterObjectsView(
    [NotEmpty] OneOrMore<int> objTypeIDs,
    [NotEmpty, ItemNotNull] OneOrMore<NavigatorViewDescriptorBase> views)
  {
    IViewsProvider provider = (IViewsProvider) new NavigatorViewsProvider<IDBTypedObjectID>(views);
    lock (Intermech.Extensions.Navigator._registerObjectViewSync)
    {
      foreach (int objTypeId in objTypeIDs)
        Intermech.Client.Services.Factory.AddViewsProvider(1, objTypeId, provider);
      Intermech.Extensions.Navigator._viewsToCheckRegistered.AddRange<NavigatorViewDescriptorBase>((IEnumerable<NavigatorViewDescriptorBase>) views);
      if (Intermech.Extensions.Navigator._pluginsLoadCompleteHandlerSet)
        return;
      Services.PluginManager.LoadComplete += new EventHandler(Intermech.Extensions.Navigator.PluginManager_LoadComplete);
      Intermech.Extensions.Navigator._pluginsLoadCompleteHandlerSet = true;
    }
  }

  private static void PluginManager_LoadComplete([CanBeNull] object sender, [NotNull] EventArgs e)
  {
    Services.PluginManager.LoadComplete -= new EventHandler(Intermech.Extensions.Navigator.PluginManager_LoadComplete);
    foreach (NavigatorViewDescriptorBase viewDescriptorBase in Intermech.Extensions.Navigator._viewsToCheckRegistered)
      viewDescriptorBase.CheckViewRegistered();
  }

  public static void RegisterViewForObjectTypes<TView>([NotEmpty] OneOrMore<int> objTypeIDs) where TView : IView
  {
    Intermech.Extensions.Navigator.RegisterObjectsView(objTypeIDs, (OneOrMore<Type>) typeof (TView));
  }

  public static void RegisterObjectsView([NotEmpty] OneOrMore<int> objTypeIDs, [NotEmpty, ItemNotNull] OneOrMore<Type> viewTypes)
  {
    if (viewTypes.Count == 1)
    {
      NavigatorObjectViewDescriptor navigatorViewDescriptor = viewTypes.First<Type>().GetNavigatorViewDescriptor<NavigatorObjectViewDescriptor>();
      Intermech.Extensions.Navigator.RegisterObjectsView(objTypeIDs, (OneOrMore<NavigatorViewDescriptorBase>) (NavigatorViewDescriptorBase) navigatorViewDescriptor);
    }
    else
    {
      NavigatorObjectViewDescriptor[] views = new NavigatorObjectViewDescriptor[viewTypes.Count];
      foreach ((int index, Type type) in viewTypes.IndexIteration<Type>())
        views[index] = type.GetNavigatorViewDescriptor<NavigatorObjectViewDescriptor>();
      Intermech.Extensions.Navigator.RegisterObjectsView(objTypeIDs, (OneOrMore<NavigatorViewDescriptorBase>) (NavigatorViewDescriptorBase[]) views);
    }
  }
}
