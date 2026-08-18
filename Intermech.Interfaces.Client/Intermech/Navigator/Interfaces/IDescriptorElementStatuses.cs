// Decompiled with JetBrains decompiler
// Type: Intermech.Navigator.Interfaces.IDescriptorElementStatuses
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

#nullable disable
namespace Intermech.Navigator.Interfaces;

/// <summary>
/// Интерфейс позволяет получать и назначать статусы у дескриптора узла.
/// Предназначен для реализации в дескрипторах корневых узлов, которые могут иметь состав.
/// Обращение к интерфейсу будет выполняться в случае, если на основании дескриптора
/// будет строиться содержимое дерева "Навигатора"
/// </summary>
public interface IDescriptorElementStatuses
{
  /// <summary>
  /// Дескриптор элемента пространства навигации, чьи статусы управляются данным интерфейсом
  /// </summary>
  IDescriptor RootDescriptor { get; }

  /// <summary>
  /// Статусы элемента пространства навигации. Установка и чтение отдельных полей должно выполняться
  /// с помощью службы IElementStatusesClientService
  /// </summary>
  byte[] Statuses { get; set; }
}
