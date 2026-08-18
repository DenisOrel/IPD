
// Type: Intermech.Navigator.ContextMenu.ItemsLink
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Описывает фрагмент односвязного списка, содержащего коллекции
/// элементов навигации, представляющие область действия команды.
/// </summary>
public class ItemsLink
{
  private ISelectedItems _items;
  private ItemsLink _next;

  /// <summary>
  /// Создает новый фрагмент списка, который будет содержать
  /// указанную коллекцию элементов навигации.
  /// </summary>
  /// <param name="items">Коллекция элементов навигации</param>
  public ItemsLink(ISelectedItems items)
  {
    Services.Check(items);
    this._items = items;
    this._next = (ItemsLink) null;
  }

  /// <summary>
  /// Возвращает коллекцию элементов навигации, которая
  /// содержится в этом фрагменте списка.
  /// </summary>
  public ISelectedItems Items => this._items;

  /// <summary>Возвращает следующий фрагмент списка.</summary>
  public ItemsLink Next
  {
    get => this._next;
    set => this._next = value;
  }
}
