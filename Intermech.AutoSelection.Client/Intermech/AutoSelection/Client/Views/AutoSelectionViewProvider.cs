// Decompiled with JetBrains decompiler
// Type: Intermech.AutoSelection.Client.Views.AutoSelectionViewProvider
// Assembly: Intermech.AutoSelection.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 0149601B-82FF-44EF-927D-3DECB2C1F37D
// Assembly location: D:\IPS\Client\Intermech.AutoSelection.Client.dll

using Intermech.Navigator.Interfaces;
using Intermech.Navigator.Views;
using System;

#nullable disable
namespace Intermech.AutoSelection.Client.Views;

internal class AutoSelectionViewProvider : IViewsProvider
{
  public ViewsInfo GetViews(ISelectedItems items, IServiceProvider services)
  {
    if (!items.Count.Equals(1))
      return ViewsInfo.Empty;
    ViewsInfo views = new ViewsInfo();
    views.Add(LocalizationHolder.rm.GetString("AutoSelection.Client_66"), new ViewInfo(0, 1452, typeof (AutoSelectionView)));
    return views;
  }
}
