// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.NavigatorViewPlacement
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>Допустимые места для отображения закладки</summary>
[Flags]
public enum NavigatorViewPlacement
{
  /// <summary>Закладку можно отображать в основном списке закладок</summary>
  InMainViews = 0,
  /// <summary>
  /// Закладку можно отображать в списке закладок, расположенном под деревом "Навигатора"
  /// </summary>
  InTreeViews = 1,
}
