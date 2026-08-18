// Decompiled with JetBrains decompiler
// Type: Intermech.Extensions.SystemObjectTypeExtensions
// Assembly: Intermech.Extensions.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 8EE4EE90-67E9-496B-9E84-18C409B882FC
// Assembly location: D:\IPS\Client\Intermech.Extensions.Client.dll

using Intermech.Diagnostics;
using Intermech.Metadata;
using Intermech.Navigator.Views;
using System;
using System.Drawing;
using System.Runtime.CompilerServices;

#nullable disable
namespace Intermech.Extensions;

public static class SystemObjectTypeExtensions
{
  [CanBeNull]
  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static Icon GetIcon([NotNull] this SystemObjectType objectType)
  {
    objectType.CheckIsLoaded();
    return Intermech.Client.Services.IconService.GetIcon(4, (int) (IpsMetadataEntityBase<int>) objectType);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void AddViews([NotNull] this SystemObjectType systemObjectType, [NotEmpty] OneOrMore<Type> views)
  {
    if (!systemObjectType.Loaded)
      return;
    Intermech.Extensions.Navigator.RegisterObjectsView((OneOrMore<int>) (IpsMetadataEntityBase<int>) systemObjectType, views);
  }

  [MethodImpl(MethodImplOptions.AggressiveInlining)]
  public static void AddView<TView>([NotNull] this SystemObjectType systemObjectType) where TView : IView
  {
    if (!systemObjectType.Loaded)
      return;
    Intermech.Extensions.Navigator.RegisterObjectsView((OneOrMore<int>) (IpsMetadataEntityBase<int>) systemObjectType, (OneOrMore<Type>) typeof (TView));
  }
}
