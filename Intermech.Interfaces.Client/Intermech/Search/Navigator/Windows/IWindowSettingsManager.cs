// Decompiled with JetBrains decompiler
// Type: Intermech.Search.Navigator.Windows.IWindowSettingsManager
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Search.Navigator.Windows;

/// <summary>Интерфейс менеджера настроек окон навигатора</summary>
public interface IWindowSettingsManager
{
  /// <summary>Взять</summary>
  /// <param name="categoryID">Категория узла навигатора</param>
  /// <param name="typeID">Тип узла навигатора</param>
  /// <returns>Настройки окон навигатора</returns>
  WindowSettingsBase Get(int categoryID, int typeID);

  /// <summary>Пакласть</summary>
  /// <param name="categoryID">Категория узла навигатора</param>
  /// <param name="typeID">Тип узла навигатора</param>
  /// <param name="settings">Настройки окон навигатора</param>
  void Set(int categoryID, int typeID, WindowSettingsBase settings);
}
