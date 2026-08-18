// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.NavigatorViewContext
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>Режим отображения закладок</summary>
public enum NavigatorViewContext
{
  /// <summary>
  /// Закладки, расположенные справа от дерева "Навигатора" (основная коллекция закладок)
  /// </summary>
  MainViews,
  /// <summary>Закладки, расположенные под деревом "Навигатора"</summary>
  TreeViews,
}
