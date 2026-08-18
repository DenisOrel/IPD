// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IKeyedSelectedItems
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс предоставляет доступ к коллекции выбранных пользователем элементов пространства навигации,
/// при этом каждый выделенный элемент снабжён своим уникальным ключом, по которому коллекция может
/// найти его абсолютный индекс
/// </summary>
public interface IKeyedSelectedItems : ISelectedItems, ISimpleSelectedItems
{
  /// <summary>
  /// Отыскать индекс элемента коллекции, которому назначен указанный ключ
  /// </summary>
  /// <param name="key">Ключ искомого элемента коллекции</param>
  /// <returns>Индекс или -1, если элемент с указанным ключом не найден в коллекции</returns>
  int GetItemIndex(string key);

  /// <summary>Отыскать ключ элемента коллекции с указанным индексом</summary>
  /// <param name="index">Индекс элемента коллекции</param>
  /// <returns>Ключ элемента коллекции</returns>
  string GetItemKey(int index);
}
