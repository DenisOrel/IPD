
// Type: Intermech.Navigator.Controls.IViewsContainer
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Controls;

/// <summary>
/// Позволяет связать коллекцию закладок навигатора, отображаемых в менеджере
/// закладок, с объектов, в котором эти закладки физически размещен.
/// </summary>
public interface IViewsContainer
{
  /// <summary>Возвращает количество закладок.</summary>
  int Count { get; }

  /// <summary>Возвращает указанную закладку.</summary>
  /// <param name="index">Индекс закладки</param>
  /// <returns>Закладка навигатора</returns>
  IViewPage this[int index] { get; }
}
