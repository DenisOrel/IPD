// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.NavigatorViewOptions
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>
/// Контекст, в рамках которого создана и отображается закладка
/// </summary>
public sealed class NavigatorViewOptions
{
  /// <summary>Режим отображения закладок</summary>
  private NavigatorViewContext _context;

  /// <summary>Режим отображения закладок</summary>
  public NavigatorViewContext Context => this._context;

  /// <summary>Создать пустой экземпляр класса</summary>
  public NavigatorViewOptions()
  {
  }

  /// <summary>Создать заполненный экземпляр класса</summary>
  /// <param name="context">Режим отображения закладок</param>
  public NavigatorViewOptions(NavigatorViewContext context) => this._context = context;
}
