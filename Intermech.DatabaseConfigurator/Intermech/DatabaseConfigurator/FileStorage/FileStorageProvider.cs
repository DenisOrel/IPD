// Decompiled with JetBrains decompiler
// Type: Intermech.DatabaseConfigurator.FileStorage.FileStorageProvider
// Assembly: Intermech.DatabaseConfigurator, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: EA0AF6EA-EE29-4FDF-AAED-DB84FDED5E5C
// Assembly location: D:\IPS\Client\Intermech.DatabaseConfigurator.dll

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.DatabaseConfigurator.FileStorage;

internal class FileStorageProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count <= 0)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Suppress("ObjectVisualizer", 0);
    views.Suppress("ChildrenView", 0);
    views.Add("ObjectFiles.FileStorageView", new ViewInfo(0, 1123, typeof (FileStorageView)));
    return views;
  }
}
