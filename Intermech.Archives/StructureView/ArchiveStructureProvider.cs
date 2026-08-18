// Decompiled with JetBrains decompiler
// Type: Intermech.Archives.StructureView.ArchiveStructureProvider
// Assembly: Intermech.Archives, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7A7AF78B-246B-41D0-A324-6D6817C18237
// Assembly location: D:\IPS\Client\Intermech.Archives.dll
// XML documentation location: D:\IPS\Client\Intermech.Archives.xml

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.Archives.StructureView;

/// <summary>провайдер для регистарции закладки - Структура архива</summary>
internal class ArchiveStructureProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    ViewsInfo views = new ViewsInfo();
    if (items == null || items.Count != 1)
      return ViewsInfo.Empty;
    views.Add("ArchiveStructureView", new ViewInfo(0, 1256, typeof (ArchiveStructureView)));
    return views;
  }
}
