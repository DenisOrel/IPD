// Decompiled with JetBrains decompiler
// Type: Intermech.Workflow.Client.SchemeViewsProvider
// Assembly: Intermech.Workflow.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 69C148DA-C200-403A-9CDB-2C809AA0D654
// Assembly location: D:\IPS\Client\Intermech.Workflow.Client.dll

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Workflow.Client;

internal class SchemeViewsProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    ViewsInfo views = new ViewsInfo();
    views.Add("ObjectVisualizer", new ViewInfo(0, 1272, typeof (SchemeView)));
    views.Add("ObjectFiles", new ViewInfo(0));
    return views;
  }
}
