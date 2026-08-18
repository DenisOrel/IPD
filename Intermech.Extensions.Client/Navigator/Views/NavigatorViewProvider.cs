// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.NavigatorViewProvider
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;
using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Navigator.Views;

public abstract class NavigatorViewProvider : IViewsProvider
{
  public abstract ViewsInfo GetViews([CanBeNull] ISelectedItems items, [NotNull] IServiceProvider services);
}
