
// Type: Intermech.Navigator.Controls.ICurrentSelectedItemsHost
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Controls;

/// <summary>
/// Интерфейс, позволяющий дочерним элементам управления менять у родительского текущую коллекцию элементов навигации
/// </summary>
public interface ICurrentSelectedItemsHost
{
  /// <summary>
  /// Текущая коллекция элементов навигации у родительского элемента управления
  /// </summary>
  ISelectedItemsHost ItemsHost { get; set; }
}
