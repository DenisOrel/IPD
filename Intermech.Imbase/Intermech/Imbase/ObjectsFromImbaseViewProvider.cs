// Decompiled with JetBrains decompiler
// Type: Intermech.Imbase.ObjectsFromImbaseViewProvider
// Assembly: Intermech.Imbase, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B12CD663-B7B7-4070-A151-D49A113FFC31
// Assembly location: D:\IPS\Client\Intermech.Imbase.dll

using Intermech.Navigator.Controls;
using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Imbase;

public class ObjectsFromImbaseViewProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider provider)
  {
    ViewsInfo views = new ViewsInfo();
    if (items != null && items.Count > 0)
      views.Add("ChildrenView", new ViewInfo(0, typeof (ChildrenView)));
    return views;
  }
}
