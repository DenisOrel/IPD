
// Type: Intermech.Navigator.Controls.ISelectedItemsModeHost
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Controls;

/// <summary>
/// Интерфейс, позволяющий дочерним элементам управления менять у родительского текущий режим выбора элементов
/// </summary>
public interface ISelectedItemsModeHost
{
  /// <summary>Текущий режим выбора элементов</summary>
  SelectedItemsMode ItemsMode { get; set; }
}
