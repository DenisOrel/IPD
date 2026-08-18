// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.DocumsProvider
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Archives;

internal class DocumsProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (items.Count != 1)
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add("ArchivesDocuments", new ViewInfo(0, 712, typeof (DocumsObject)));
    views.Suppress("ObjectVisualizer", 0);
    views.Suppress("ObjectFiles", 0);
    return views;
  }
}
