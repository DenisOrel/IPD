// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.ICurrentNavWindow
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>Интерфейс на текущее окно навигатора</summary>
public interface ICurrentNavWindow
{
  /// <summary>
  /// Текущее окно навигатора (или null, если такового нету)
  /// </summary>
  object NavWindow { get; set; }

  /// <summary>
  /// Ссылка на дерево "Навигатора" (привести к типу NavigatorTreeView)
  /// </summary>
  object TreeView { get; set; }

  /// <summary>
  /// Ссылка на менеджер закладок (привести к типу PageViewsManager)
  /// </summary>
  object ViewsManagers { get; set; }
}
