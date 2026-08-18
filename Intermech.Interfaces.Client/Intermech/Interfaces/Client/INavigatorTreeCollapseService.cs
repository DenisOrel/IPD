// Decompiled with JetBrains decompiler
// Type: Intermech.Interfaces.Client.INavigatorTreeCollapseService
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
/// требуется ли сворачивать дерево "Навигатора" при открытии нового окна
/// </summary>
public interface INavigatorTreeCollapseService
{
  /// <summary>
  /// Зарегистрировать класс, позволяющий проверять дескрипторы
  /// </summary>
  /// <param name="selector">Класс, выполняющий проверки дескрипторов</param>
  void Register(IEnableTreeCollapse selector);

  /// <summary>
  /// Разрегистрировать класс, позволяющий проверять дескрипторы
  /// </summary>
  /// <param name="selector">Класс, выполняющий проверки дескрипторов</param>
  void Unregister(IEnableTreeCollapse selector);

  /// <summary>
  /// Выполнить проверку, можно ли сворачивать дерево "Навигатора" при открытии нового окна,
  /// которое построено на основании указанного дескриптора корневого узла
  /// </summary>
  /// <param name="rootDescriptor">Дескриптор корневого узла дерева</param>
  /// <param name="viewServices">Контейнер сервисов для дерева</param>
  /// <returns>Сортировка в колонках дерева разрешена или нет</returns>
  bool EnableTreeCollapse(IDescriptor rootDescriptor, IServiceProvider viewServices);
}
