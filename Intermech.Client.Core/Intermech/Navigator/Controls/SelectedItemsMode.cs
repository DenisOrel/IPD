
// Type: Intermech.Navigator.Controls.SelectedItemsMode
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Controls;

/// <summary>Режим выбора элементов</summary>
public enum SelectedItemsMode
{
  /// <summary>
  /// Режим выбора по умолчанию.
  /// Возвращаем CheckedItems если они есть, иначе FocusedItems
  /// </summary>
  Default,
  /// <summary>Возвращаем только FocusedItems</summary>
  FocusedItems,
  /// <summary>Возвращаем только CheckedItems</summary>
  CheckedItems,
}
