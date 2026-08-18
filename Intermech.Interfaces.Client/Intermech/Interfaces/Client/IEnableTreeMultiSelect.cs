// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IEnableTreeMultiSelect
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
/// можно ли разрешать множественное выделение в дереве "Навигатора"
/// для содержимого, которое строится на основе указанного дескриптора
/// корневого узла
/// </summary>
public interface IEnableTreeMultiSelect
{
  /// <summary>Уникальный Guid класса, реализующего данный интерфейс</summary>
  Guid Guid { get; }

  /// <summary>
  /// Выполнить проверку, можно ли включить множественное выделение в дереве "Навигатора",
  /// которое построено на основании указанного дескриптора корневого узла
  /// </summary>
  /// <param name="rootDescriptor">Дескриптор корневого узла дерева</param>
  /// <param name="viewServices">Контейнер сервисов для дерева</param>
  /// <returns>Множественное выделение разрешено, не разрешено, дескриптор не распознан</returns>
  YesNoUnknownEnum EnableTreeMultiSelect(IDescriptor rootDescriptor, IServiceProvider viewServices);
}
