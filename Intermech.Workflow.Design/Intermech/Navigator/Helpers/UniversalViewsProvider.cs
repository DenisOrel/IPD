// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Helpers.UniversalViewsProvider
// Assembly: Intermech.Workflow.Design, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: AF8177C8-0B57-4C67-8EA5-DF33FBCB2FBD
// Assembly location: D:\IPS\Client\Intermech.Workflow.Design.dll
// XML documentation location: D:\IPS\Client\Intermech.Workflow.Design.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Navigator.Helpers;

public class UniversalViewsProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add("ChildrenView", new ViewInfo(0, 0, typeof (UniversalObjectsView)));
    return views;
  }
}
