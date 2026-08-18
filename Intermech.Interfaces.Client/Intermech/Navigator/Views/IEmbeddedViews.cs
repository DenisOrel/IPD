// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Views.IEmbeddedViews
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Views;

/// <summary>
/// Интерфейс, позволяющий управлять вложенными вьюшками (реализован в ChildrenView)
/// </summary>
public interface IEmbeddedViews
{
  /// <summary>Открыта ли панель с дополнительными видами.</summary>
  bool IsOpen { get; }

  /// <summary>
  /// Позволяет управлять сфокусированной и выделенными записями во вложенных закладках
  /// </summary>
  iFocusAndSelection EmbeddedFocusAndSelection { get; set; }

  /// <summary>
  /// Выводит на экран панель с дополнительными видами. Высота вложенных закладок - по умолчанию.
  /// </summary>
  void OpenEmbeddedViews();

  /// <summary>Выводит на экран панель с дополнительными видами.</summary>
  /// <param name="height">Высота вложенных закладок (в процентном соотношении по отношению к высоте самой закладки)</param>
  void OpenEmbeddedViews(int height);

  /// <summary>Убирает с экрана панель с дополнительными видами.</summary>
  void CloseEmbeddedViews();
}
