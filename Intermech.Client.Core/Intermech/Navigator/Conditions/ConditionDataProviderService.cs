
// Type: Intermech.Navigator.Conditions.ConditionDataProviderService
// Assembly: Intermech.Client.Core, Version=7.0.2.1112, Culture=neutral, PublicKeyToken=null
// MVID: 7B8171F9-1AF1-4B71-8ADB-BCA094F21940
:\IPS\Client\Intermech.Client.Core.dll
// XML documentation location: D:\IPS\Client\Intermech.Client.Core.xml

using Intermech.Navigator.Interfaces;
using System;
using System.Collections.Generic;


namespace Intermech.Navigator.Conditions;

internal sealed class ConditionDataProviderService : IConditionDataProviderService
{
  private Dictionary<SelectionDataSource, IConditionDataProvider> _providers;

  public ConditionDataProviderService()
  {
    this._providers = new Dictionary<SelectionDataSource, IConditionDataProvider>();
  }

  public IConditionDataProvider GetDataProvider(SelectionDataSource selectionDataSource)
  {
    IConditionDataProvider dataProvider;
    if (!this._providers.TryGetValue(selectionDataSource, out dataProvider))
      throw new Exception($"Не найден провайдер для {selectionDataSource.ToString()}");
    return dataProvider;
  }

  public void Register(SelectionDataSource selectionDataSource, IConditionDataProvider dataProvider)
  {
    if (this._providers.ContainsKey(selectionDataSource))
      throw new Exception($"Для {selectionDataSource.ToString()} уже зарегистрирован провайдер!");
    this._providers.Add(selectionDataSource, dataProvider);
  }
}
