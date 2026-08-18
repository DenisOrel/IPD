
// Type: Intermech.Navigator.Controls.SelectedItemsAnalyzerResult
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Controls;

/// <summary>
/// Результат выполнения проверки коллекции выделенных элементов в службе ISelectedItemsAnalyzer
/// </summary>
public enum SelectedItemsAnalyzerResult
{
  /// <summary>
  /// Элементы коллекции не являются подходящими, кнопка "ОК" становится запрещённой
  /// </summary>
  Disabled,
  /// <summary>
  /// Элементы коллекции являются подходящими, кнопка "ОК" становится разрешённой
  /// </summary>
  Enabled,
}
