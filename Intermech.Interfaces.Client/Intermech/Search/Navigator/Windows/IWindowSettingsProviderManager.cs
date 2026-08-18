// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Navigator.Windows.IWindowSettingsProviderManager
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Search.Navigator.Windows;

/// <summary>
/// Интерфейс менеджера провайдеров настроек окон навигатора
/// </summary>
public interface IWindowSettingsProviderManager
{
  /// <summary>Взять провайдер настроек окон навигатора</summary>
  /// <param name="categoryID">Категория узла навигатора</param>
  /// <returns>Провайдер настроек окон навигатора</returns>
  IWindowSettingsProvider Get(int categoryID);

  /// <summary>Зарегистрировать провайдер настроек окон навигатора</summary>
  /// <param name="categoryID">Категория узла навигатора</param>
  /// <param name="provider">Провайдер настроек окон навигатора</param>
  void Register(int categoryID, IWindowSettingsProvider provider);
}
