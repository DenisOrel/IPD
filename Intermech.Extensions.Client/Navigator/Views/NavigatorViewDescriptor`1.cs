// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.NavigatorViewDescriptor`1
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;
using System;
using System.Collections.Generic;

#nullable disable
namespace Intermech.Navigator.Views;

public abstract class NavigatorViewDescriptor<TItem> : NavigatorViewDescriptorBase
{
  public NavigatorViewDescriptor<TItem>.CanShowForItemsDelegate Filter { get; }

  protected NavigatorViewDescriptor(
    [NotNull] Type viewType,
    [CanBeNull, NotEmpty] string name = null,
    [NotNull] string caption = "",
    [NotNull] string hint = "",
    [NotNull] string module = "",
    [NotNull] string imageName = "",
    int orderID = 0,
    int triggerPriority = 0,
    int helpTopicID = 0,
    bool supportMultipleSelection = false,
    [CanBeNull] NavigatorViewDescriptor<TItem>.CanShowForItemsDelegate filter = null)
    : base(viewType, name, caption, hint, module, imageName, orderID, triggerPriority, helpTopicID, supportMultipleSelection)
  {
    this.Filter = filter;
  }

  public delegate bool CanShowForItemsDelegate(
    [NotNull] IServiceProvider services,
    [NotNull] IReadOnlyCollection<TItem> selectedItem);
}
