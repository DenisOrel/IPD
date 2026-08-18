// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.IEnableTreeMultiSelectService
// Assembly: Intermech.Interfaces.Client, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: B76D4270-8411-4D02-AE4F-B51CD7FF3A46
// Assembly location: D:\IPS\Client\Intermech.Interfaces.Client.dll
// XML documentation location: D:\IPS\Client\Intermech.Interfaces.Client.xml

using Intermech.Navigator.Interfaces;
using System;

#nullable disable
namespace Intermech.Interfaces.Client;

/// <summary>
/// Интерфейс клиентской службы, позволяющей окнам "Навигатора" определять,
/// можно ли разрешать множественное выделение в дереве "Навигатора"
/// для содержимого, которое строится на основе указанного дескриптора
/// корневого узла
/// </summary>
public interface IEnableTreeMultiSelectService
{
  /// <summary>
  /// Зарегистрировать класс, позволяющий проверять дескрипторы
  /// </summary>
  /// <param name="selector">Класс, выполняющий проверки дескрипторов</param>
  void Register(IEnableTreeMultiSelect selector);

  /// <summary>
  /// Разрегистрировать класс, позволяющий проверять дескрипторы
  /// </summary>
  /// <param name="selector">Класс, выполняющий проверки дескрипторов</param>
  void Unregister(IEnableTreeMultiSelect selector);

  /// <summary>
  /// Выполнить проверку, можно ли включить множественное выделение в дереве "Навигатора",
  /// которое построено на основании указанного дескриптора корневого узла
  /// </summary>
  /// <param name="rootDescriptor">Дескриптор корневого узла дерева</param>
  /// <param name="viewServices">Контейнер сервисов для дерева</param>
  /// <returns>Множественное выделение разрешено или нет</returns>
  bool EnableTreeMultiSelect(IDescriptor rootDescriptor, IServiceProvider viewServices);
}
