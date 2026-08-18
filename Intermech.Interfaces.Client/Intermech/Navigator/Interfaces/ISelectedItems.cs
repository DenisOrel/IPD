// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.ISelectedItems
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс предоставляет доступ к коллекции выбранных пользователем элементов пространства навигации
/// </summary>
public interface ISelectedItems : ISimpleSelectedItems
{
  /// <summary>
  /// Возвращает true, если коллекция содержит разнородные идентификаторы
  /// элементов (т.е. созданные разными элементами навигации). Такие
  /// разнородные коллекции образуются при множественном выделении в дереве
  /// навигатора и других подобных этой ситуациях.
  /// </summary>
  bool IsCollage { get; }

  /// <summary>Возвращает идентификатор элемента в коллекции.</summary>
  /// <param name="index">Индекс идентификатора элемента в коллекции.</param>
  /// <returns>Идентификатор элемента.</returns>
  INodeID GetItemID(int index);

  /// <summary>
  /// Возвращает данные требуемого формата для родительского элемента,
  /// создавшего указанный идентификатор элемента. Если родительский элемент
  /// не поддерживает запрошенный формат данных, то результатом будет null.
  /// </summary>
  /// <param name="index">Индекс идентификатора элемента в коллекции.</param>
  /// <param name="dataFormat">Тип формата данных.</param>
  /// <returns>Данные в указанном формате.</returns>
  object GetParentData(int index, Type dataFormat);

  /// <summary>
  /// Возвращает полный путь родительского элемента для указанного
  /// идентификатора в коллекции.
  /// </summary>
  /// <param name="index">Индекс идентификатора элемента в коллекции.</param>
  /// <returns>Путь родительского элемента.</returns>
  NodeIDPath GetParentPath(int index);
}
