// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Navigator.Windows.IWindowSettingsProvider
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Search.Navigator.Windows;

/// <summary>
/// <para>Интерфейс провайдера настроек окон навигатора</para>
/// <para>Используется для переопределения алгоритма извлечения/палажения настроек из/в коллекции/ю для категории узлов</para>
/// </summary>
public interface IWindowSettingsProvider
{
  /// <summary>Взять</summary>
  /// <param name="typeID">Тип узла навигатора</param>
  /// <param name="collection">Коллекция всех настроек окон навигатора</param>
  /// <returns></returns>
  WindowSettingsBase Get(int typeID, WindowSettingsCollection collection);

  /// <summary>Положить</summary>
  /// <param name="typeID">Тип узла навигатора</param>
  /// <param name="settings">Настройки окон навигатора</param>
  /// <param name="collection">Коллекция всех настроек окон навигатора</param>
  void Set(int typeID, WindowSettingsBase settings, WindowSettingsCollection collection);
}
