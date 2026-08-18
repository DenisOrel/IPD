
// Type: Intermech.Navigator.ContextMenu.StepwiseProviderManager
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;


namespace Intermech.Navigator.ContextMenu;

/// <summary>
/// Реализует менеджер, который управляет работой пошаговых провайдеров команд.
/// </summary>
public class StepwiseProviderManager
{
  private StepwiseProviderCollection _providers = new StepwiseProviderCollection();

  /// <summary>
  /// Возвращает коллекцию провайдеров, которой управляет менеджер.
  /// </summary>
  public StepwiseProviderCollection Providers => this._providers;

  /// <summary>
  /// Собирает информацию о командах у провайдеров и помещает ее в контейнер.
  /// </summary>
  /// <param name="items">Коллекция элементов навигации, выбранных пользователем</param>
  /// <param name="viewServices">Контейнер с дополнительными сервисами</param>
  /// <param name="commandsInfo">Контейнер с информацией о командах</param>
  public void CollectCommands(
    ISelectedItems items,
    IServiceProvider viewServices,
    CommandsInfo commandsInfo)
  {
    Services.Check(items);
    Services.Check(viewServices);
    Services.Check(commandsInfo);
    StepwiseProviderCollection providerCollection = new StepwiseProviderCollection();
    for (int index = 0; index < this._providers.Count; ++index)
    {
      this._providers[index].Preprocess(items, viewServices);
      if (this._providers[index].CanContinue)
        providerCollection.Add(this._providers[index]);
    }
    for (int index1 = 0; index1 < items.Count; ++index1)
    {
      int index2 = 0;
      while (index2 < providerCollection.Count)
      {
        providerCollection[index2].Process(items, index1);
        if (providerCollection[index2].CanContinue)
          ++index2;
        else
          providerCollection.RemoveAt(index2);
      }
      if (providerCollection.Count == 0)
        break;
    }
    for (int index = 0; index < this._providers.Count; ++index)
      this._providers[index].Postprocess(commandsInfo);
  }
}
