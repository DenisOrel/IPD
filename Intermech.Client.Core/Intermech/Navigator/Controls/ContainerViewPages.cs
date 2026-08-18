
// Type: Intermech.Navigator.Controls.ContainerViewPages
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml


namespace Intermech.Navigator.Controls;

/// <summary>
/// Коллекция закладок навигатора, физически хранящихся в другом объекте.
/// </summary>
public sealed class ContainerViewPages : ViewPages
{
  private IViewsContainer container;

  /// <summary>Создает коллекцию.</summary>
  /// <param name="container">Объект, который физически содержит закладки</param>
  public ContainerViewPages(IViewsContainer container) => this.container = container;

  /// <summary>Возвращает количество закладок.</summary>
  public override int Count => this.container.Count;

  /// <summary>Возвращает указанную закладку.</summary>
  /// <param name="index">Индекс закладки</param>
  /// <returns>Закладка навигатора</returns>
  public override IViewPage this[int index] => this.container[index];
}
