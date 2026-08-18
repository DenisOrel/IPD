// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IDescriptorElementStatusesService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс службы, которая позволяет подписаться на событие для добавления
/// статусов в дескрипторы корневых узлов дерева "Навигатора"
/// </summary>
public interface IDescriptorElementStatusesService
{
  /// <summary>
  /// Событие "Установить/изменить значения в статусах дескриптора корневого элемента пространства навигации"
  /// </summary>
  event Intermech.Navigator.Interfaces.SetDescriptorStatuses SetDescriptorStatuses;

  /// <summary>
  /// Сгенерировать событие "Установить/изменить значения в статусах дескриптора корневого элемента пространства навигации"
  /// </summary>
  /// <param name="descriptor">Интерфейс, позволяющий управлять статусами дескриптора корневого элемента пространства навигации</param>
  void FireSetDescriptorStatuses(IDescriptorElementStatuses descriptor);
}
