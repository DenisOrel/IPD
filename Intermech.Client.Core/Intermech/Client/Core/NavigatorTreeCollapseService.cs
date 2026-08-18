
// Type: Intermech.Client.Core.NavigatorTreeCollapseService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Interfaces.Client;
using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Client.Core;

/// <summary>
/// Клиентская служба, позволяющая окнам "Навигатора" определять,
/// требуется ли сворачивать дерево "Навигатора" при открытии нового окна
/// </summary>
internal class NavigatorTreeCollapseService : INavigatorTreeCollapseService
{
  /// <summary>
  /// Коллекция интерфейсов, которые позволяют выполнить анализ дескриптора
  /// </summary>
  private Dictionary<Guid, IEnableTreeCollapse> _selectors = new Dictionary<Guid, IEnableTreeCollapse>();

  /// <summary>
  /// Зарегистрировать класс, позволяющий проверять дескрипторы
  /// </summary>
  /// <param name="selector">Класс, выполняющий проверки дескрипторов</param>
  public void Register(IEnableTreeCollapse selector)
  {
    if (selector == null)
      return;
    this._selectors[selector.Guid] = selector;
  }

  /// <summary>
  /// Разрегистрировать класс, позволяющий проверять дескрипторы
  /// </summary>
  /// <param name="selector">Класс, выполняющий проверки дескрипторов</param>
  public void Unregister(IEnableTreeCollapse selector)
  {
    if (selector == null || !this._selectors.ContainsKey(selector.Guid))
      return;
    this._selectors.Remove(selector.Guid);
  }

  /// <summary>
  /// Выполнить проверку, можно ли сворачивать дерево "Навигатора" при открытии нового окна,
  /// которое построено на основании указанного дескриптора корневого узла
  /// </summary>
  /// <param name="rootDescriptor">Дескриптор корневого узла дерева</param>
  /// <param name="viewServices">Контейнер сервисов для дерева</param>
  /// <returns>Сортировка в колонках дерева разрешена или нет</returns>
  public bool EnableTreeCollapse(IDescriptor rootDescriptor, IServiceProvider viewServices)
  {
    if (this._selectors.Count == 0)
      return true;
    int num1 = 0;
    int num2 = 0;
    foreach (KeyValuePair<Guid, IEnableTreeCollapse> selector in this._selectors)
    {
      switch (selector.Value.EnableTreeCollapse(rootDescriptor, viewServices))
      {
        case YesNoUnknownEnum.No:
          ++num2;
          break;
        case YesNoUnknownEnum.Yes:
          ++num1;
          break;
      }
      if (num1 > 0)
        return true;
    }
    return false;
  }
}
