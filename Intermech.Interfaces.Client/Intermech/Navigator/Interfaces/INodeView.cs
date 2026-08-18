// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.INodeView
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System.Collections;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс позволяет получать доступ к коллекции узлов INodeID у элемента управления
/// </summary>
public interface INodeView
{
  /// <summary>Количество узлов</summary>
  int Count { get; }

  /// <summary>Получить узел с указанным индексом</summary>
  /// <param name="index">Индекс узла</param>
  /// <returns></returns>
  INodeID this[int index] { get; }

  /// <summary>Добавить в коллекцию дополнительные узлы</summary>
  /// <param name="partialNodeIDs">Коллекция дополнительных узлов</param>
  void Append(NodeIDCollection partialNodeIDs);

  /// <summary>Обновить коллекцию узлов с указанными индексами</summary>
  /// <param name="indexes">Коллекция индексов узлов, которые требуется обновить</param>
  void Update(IList indexes);

  /// <summary>
  /// Выполнить замену узлов с указанными индексами данными из дополнительной коллекции
  /// </summary>
  /// <param name="indexes">Коллекция индексов узлов, которые требуется заменить</param>
  /// <param name="replacementNodeIDs">Коллекция новых узлов взамен старых</param>
  void Replace(IList indexes, NodeIDCollection replacementNodeIDs);

  /// <summary>Удалить узлы с указанными индексами</summary>
  /// <param name="indexes">Коллекция индексов узлов, которые требуется удалить</param>
  void Remove(IList indexes);
}
