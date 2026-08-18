// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.NavigatorObjectViewDescriptor
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.DataFormats;
using Intermech.Diagnostics;
using System;

#nullable disable
namespace Intermech.Navigator.Views;

public class NavigatorObjectViewDescriptor(
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
  [CanBeNull] NavigatorViewDescriptor<IDBTypedObjectID>.CanShowForItemsDelegate filter = null) : 
  NavigatorViewDescriptor<IDBTypedObjectID>(viewType, name, caption, hint, module, imageName, orderID, triggerPriority, helpTopicID, supportMultipleSelection, filter)
{
}
