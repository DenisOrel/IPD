// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IPropertyPageActivationEvents
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Необязательное расширение для <see cref="T:Intermech.Interfaces.Client.IPropertyPage" />, позволяющее обрабатывать события
/// переключения между страницами в окне параметров настройки.
/// </summary>
public interface IPropertyPageActivationEvents : IPropertyPage
{
  /// <summary>
  /// Вызывается при инициализации окна параметров настройки до отображения окна.
  /// Метод не должен бросать исключений.
  /// </summary>
  void InitializePage();

  /// <summary>
  /// Вызывается перед переключением на текущую страницу.
  /// Метод не должен бросать исключений.
  /// </summary>
  void BeforeActivatePage();

  /// <summary>
  /// Вызывается после переключения на текущую страницу.
  /// Метод не должен бросать исключений.
  /// </summary>
  void AfterActivatePage();

  /// <summary>
  /// Вызывается перед переключением с текущей страницы на другую страницу.
  /// Метод не должен бросать исключений.
  /// </summary>
  void BeforeDeactivatePage();

  /// <summary>
  /// Вызывается после переключения с текущей страницы на другую страницу.
  /// Метод не должен бросать исключений.
  /// </summary>
  void AfterDeactivatePage();
}
