
// Type: Intermech.Client.Core.EnableTreeMultiSelectService
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
/// можно ли разрешать множественное выделение в дереве "Навигатора"
/// для содержимого, которое строится на основе указанного дескриптора
/// корневого узла
/// </summary>
internal class EnableTreeMultiSelectService : IEnableTreeMultiSelectService
{
  /// <summary>
  /// Коллекция интерфейсов, которые позволяют выполнить анализ дескриптора
  /// </summary>
  private Dictionary<Guid, IEnableTreeMultiSelect> _selectors = new Dictionary<Guid, IEnableTreeMultiSelect>();

  /// <summary>
  /// Зарегистрировать класс, позволяющий проверять дескрипторы
  /// </summary>
  /// <param name="selector">Класс, выполняющий проверки дескрипторов</param>
  public void Register(IEnableTreeMultiSelect selector)
  {
    if (selector == null)
      return;
    this._selectors[selector.Guid] = selector;
  }

  /// <summary>
  /// Разрегистрировать класс, позволяющий проверять дескрипторы
  /// </summary>
  /// <param name="selector">Класс, выполняющий проверки дескрипторов</param>
  public void Unregister(IEnableTreeMultiSelect selector)
  {
    if (selector == null || !this._selectors.ContainsKey(selector.Guid))
      return;
    this._selectors.Remove(selector.Guid);
  }

  /// <summary>
  /// Выполнить проверку, можно ли включить множественное выделение в дереве "Навигатора",
  /// которое построено на основании указанного дескриптора корневого узла
  /// </summary>
  /// <param name="rootDescriptor">Дескриптор корневого узла дерева</param>
  /// <param name="viewServices">Контейнер сервисов для дерева</param>
  /// <returns>Множественное выделение разрешено или нет</returns>
  public bool EnableTreeMultiSelect(IDescriptor rootDescriptor, IServiceProvider viewServices)
  {
    if (this._selectors.Count == 0)
      return false;
    int num1 = 0;
    int num2 = 0;
    foreach (KeyValuePair<Guid, IEnableTreeMultiSelect> selector in this._selectors)
    {
      switch (selector.Value.EnableTreeMultiSelect(rootDescriptor, viewServices))
      {
        case YesNoUnknownEnum.No:
          ++num2;
          break;
        case YesNoUnknownEnum.Yes:
          ++num1;
          break;
      }
      if (num2 > 0)
        return false;
    }
    return num1 != 0;
  }
}
