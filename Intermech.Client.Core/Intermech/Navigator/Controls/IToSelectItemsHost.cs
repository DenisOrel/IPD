
// Type: Intermech.Navigator.Controls.IToSelectItemsHost
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Controls;

/// <summary>
/// Интерфейс, позволяющий принудительно перестроить в контроле коллекцию выделенных
/// элементов, если контрол поддерживает работу с сервисом IToSelectItemsAnalyzers
/// </summary>
public interface IToSelectItemsHost
{
  /// <summary>
  /// Обновить коллекцию выделенных элементов на основе сервиса IToSelectItemsAnalyzers
  /// </summary>
  void RefreshWithToSelectItemsAnalyzers();
}
