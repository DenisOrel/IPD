// Decompiled with JetBrains decompiler
// Type: Intermech.Project.Client.ProjectViewsProvider
// Assembly: Intermech.Project.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: D968BDD9-29F0-4E24-8F57-6E851EE47258
// Assembly location: D:\IPS\Client\Intermech.Project.Client.dll

using Intermech.Diagnostics;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Project.Client;

internal class ProjectViewsProvider : IViewsProvider
{
  [NotNull]
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    ViewsInfo views = new ViewsInfo();
    views.Add("ObjectFiles", new ViewInfo(0));
    return views;
  }
}
