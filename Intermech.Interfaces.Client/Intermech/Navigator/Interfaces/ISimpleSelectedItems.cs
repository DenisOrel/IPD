// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ISimpleSelectedItems
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс предоставляет доступ к упрощённой коллекции выбранных пользователем элементов пространства навигации.
/// </summary>
public interface ISimpleSelectedItems
{
  /// <summary>
  /// Возвращает количество идентификаторов элементов навигации в коллеции.
  /// </summary>
  int Count { get; }

  /// <summary>
  /// Возвращает данные указанного формата для элемента коллекции. Если элемент
  /// не поддерживает указанный формат, то результатом будет null.
  /// </summary>
  /// <param name="index">Индекс идентификатора элемента в коллекции.</param>
  /// <param name="dataFormat">Тип формата данных.</param>
  /// <returns>Данные в указанном формате.</returns>
  object GetItemData(int index, Type dataFormat);
}
