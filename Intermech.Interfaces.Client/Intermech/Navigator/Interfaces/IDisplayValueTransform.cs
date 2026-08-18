// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IDisplayValueTransform
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using System;

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс позволяет выполнять дополнительные преобразования значений, отображаемых
/// в гридах и деревьях
/// </summary>
public interface IDisplayValueTransform
{
  /// <summary>
  /// Выполнить преобразование значения перед его отображением на экране
  /// </summary>
  /// <param name="sender">Элемент управления, для которого выполняются преобразования</param>
  /// <param name="sourceValue">Исходное значение</param>
  /// <param name="column">Колонка</param>
  /// <param name="services">Контейнер сервисов</param>
  /// <param name="handler">Обработчик для узла, содержимое которого отображается</param>
  /// <param name="nodeID">Идентификатор отображаемого элемента пространства навигации</param>
  /// <returns>Значение для вывода на экран</returns>
  object Transform(
    object sender,
    object sourceValue,
    NodeColumn column,
    IServiceProvider services,
    INode handler,
    INodeID nodeID);
}
