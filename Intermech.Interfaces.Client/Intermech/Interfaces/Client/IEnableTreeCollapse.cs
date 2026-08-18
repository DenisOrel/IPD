// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IEnableTreeCollapse
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс, позволяющий окнам "Навигатора" определять,
/// можно ли сворачивать дерево "Навигатора" при открытии нового окна,
/// содержимое которое строится на основе указанного дескриптора корневого узла
/// </summary>
public interface IEnableTreeCollapse
{
  /// <summary>Уникальный Guid класса, реализующего данный интерфейс</summary>
  Guid Guid { get; }

  /// <summary>
  /// Выполнить проверку, можно ли сворачивать дерево "Навигатора",
  /// которое построено на основании указанного дескриптора корневого узла,
  /// при открытии нового окна
  /// </summary>
  /// <param name="rootDescriptor">Дескриптор корневого узла дерева</param>
  /// <param name="viewServices">Контейнер сервисов для дерева</param>
  /// <returns>Сворачивание дерева разрешено, не разрешено, дескриптор не распознан</returns>
  YesNoUnknownEnum EnableTreeCollapse(IDescriptor rootDescriptor, IServiceProvider viewServices);
}
