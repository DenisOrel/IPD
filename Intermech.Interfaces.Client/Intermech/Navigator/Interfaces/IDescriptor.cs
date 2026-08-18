// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IDescriptor
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс, который должен реализовываться классами, описывающими различные
/// типы элементов из пространства навигации, которые можно включать в состав
/// различных виртуальных элементов (например, в корень всего дерева
/// навигатора).
/// </summary>
public interface IDescriptor : INodeItems
{
  /// <summary>
  /// Возвращает идентификатор поля источника данных для указанной
  /// виртуальной колонки. Если данная колонка не поддерживается, то
  /// метод возвращает null.
  /// </summary>
  /// <param name="column">Виртуальная колонка навигатора</param>
  /// <returns></returns>
  object MapColumnToField(NodeColumn column);

  /// <summary>
  /// Возвращает актуальный на момент вызова метода идентификатор объекта, который
  /// описывается дескриптором. Метод может возвращать null, если объект не доступен или
  /// не существует.
  /// </summary>
  /// <returns>Унифицированный идентификатор объекта</returns>
  INodeID GetRecordNodeID();

  /// <summary>
  /// Возвращает значения полей для объекта, описываемого унифицированным дескриптором.
  /// Метод может возвращать null, если объект не доступен или не существует.
  /// </summary>
  /// <param name="nodeID">Унифицированный дескриптор.</param>
  /// <param name="fields">Массив идентификаторов полей данных, значения которых
  /// должны быть получены в результате выполнения запроса.</param>
  /// <returns></returns>
  object[] GetRecordValues(INodeID nodeID, object[] fields);
}
